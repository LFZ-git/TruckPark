using ClosedXML.Excel;
using HiQPdf;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Model.Models;
using Model.Models.Invoice;
using Model.Models.UserDetail;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WEB.APIHelper;
using WEB.Helper;

namespace WEB.Controllers
{
    public class InvoiceController : Controller
    {
        public ActionResult Generate(int? organizationId)
        {
            try
            {
                List<OrganizationModel> organizationList = WebAPIHelper.CallApi<List<OrganizationModel>>(HttpMethods.Get, "GetOrganizationList", "Organization");
                ViewBag.OrganizationList = new SelectList(organizationList, "OrganizationID", "CompanyShortName");

                if (organizationId != null)
                {
                    UserDetailModel userdetail = (UserDetailModel)Session["UserDetails"];
                    List<TruckDetails> list = WebAPIHelper.CallApi<List<TruckDetails>>(HttpMethods.Get, "GetTruckCheckedOutList", "Truck", null, null, userdetail.UDID, userdetail.RoleId);
                    ViewBag.TruckList = list.Where(x => x.IsBilled == false && x.CalledByOrganizationId == organizationId);
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Something went wrong.";
                TempData["alertTitle"] = "Error";
                TempData["type"] = "error";
            }
            return View();
        }

        [HttpPost]
        public ActionResult GenerateInvoice(FormCollection formCollection)
        {
            try
            {
                string[] values = formCollection["TruckDetailsId"].Split(new char[] { ',' });
                UserDetailModel userdetail = (UserDetailModel)Session["UserDetails"];

                AddProformaInvoice objProformaInvoice = new AddProformaInvoice();
                objProformaInvoice.TotalTruckCount = values.Length;
                objProformaInvoice.createdBy = userdetail.UDID;
                objProformaInvoice.OrganizationId = Sql.ToInt(formCollection["organizationId"]);
                objProformaInvoice.TruckIdList = formCollection["TruckDetailsId"];

                ResponseInfo response = WebAPIHelper.CallApi<ResponseInfo>(HttpMethods.Post, "GenerateProformaInvoice", "Invoice", objProformaInvoice);


                if (response.ID.IsNotNull() || response.ID > 0)
                {
                    ProformaInvoice model = new ProformaInvoice();
                    string path = GenerateInvoicePdf(Sql.ToInt(response.ID));
                    model.ProformaInvoiceId = (long)response.ID;
                    model.InvoiceFilePath = path;

                    ResponseInfo res = WebAPIHelper.CallApi<ResponseInfo>(HttpMethods.Post, "AddInvoicePdf", "Invoice", model);

                }
                if (response.ID.IsNotNull() || response.ID > 0)
                {
                    InvoiceDetailsAPIModel objAPIOne = new InvoiceDetailsAPIModel();
                    objAPIOne.ProformaInvoiceId = (int)response.ID;

                    InvoiceDetailsAPIModel model1 = WebAPIHelper.CallApi<InvoiceDetailsAPIModel>(HttpMethods.Post, "GetInvoiceDetails", "Invoice", objAPIOne);


                    //----------------Invoice orion genetartion starts CALL METHOD

                    model1 = CallAPIData(model1.CustomerCode, model1.InvoiceAmount, model1.InvoiceReference);
                    //----------------Invoice orion genetartion  CALL METHOD ENDS
                }
                TempData["msg"] = "Invoice Generated Successfully.";
                TempData["alertTitle"] = "Success";
                TempData["type"] = "success";
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Something went wrong.";
                TempData["alertTitle"] = "Error";
                TempData["type"] = "error";
            }
            return RedirectToAction("Generate");
        }

        //public InvoiceDetailsAPIModel callAPI(string CustomerCode, string InvoiceAmount, string InvoiceReference)
        // {


        //----------------Invoice orion genetartion starts
        public InvoiceDetailsAPIModel CallAPIData(string CustomerCode, string InvoiceAmount, string InvoiceReference)
        {
            InvoiceDetailsAPIModel model = new InvoiceDetailsAPIModel();
            try
            {



                var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://197.149.93.70:10053/LFTZ_APIS/orderDetailsv2.php?apiKey=MTgzOTMyVFVFU0RBWSAg&companyID=LFTZ");

                httpWebRequest.Method = "POST";

                httpWebRequest.Headers.Add("apikey:MTgzOTMyVFVFU0RBWSAg");
                httpWebRequest.Headers.Add("compid:LFTZ");
                httpWebRequest.ContentType = "application/json";
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string param = "{" + "\"CustomerCode\":\"" + CustomerCode + "\"," + "\"InvoiceAmount\":\"" + InvoiceAmount + "\"," + "\"InvoiceReference\":\"" + InvoiceReference + "\"}";
                    dynamic json = JsonConvert.DeserializeObject(param);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                    }
                }
                return model;
            }
            catch (Exception ex)
            {

            }
            return model;

        }

        //----------------Invoice orion genetartion ENDs
        public string GenerateInvoicePdf(int invoiceId)
        {
            HtmlToPdf _htmlToPdf = new HtmlToPdf();
            ProformaInvoiceDetails details = WebAPIHelper.CallApi<ProformaInvoiceDetails>(HttpMethods.Get, "GetInvoiceDetails", "Invoice", null, invoiceId);

            string _systemPath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
            string filePath = Properties.Settings.Default.InvoiceTemplate;
            string templateContent = System.IO.File.ReadAllText(_systemPath + filePath);
            templateContent = templateContent.Replace("[INVOICE_NO]", details.InvoiceNo);
            templateContent = templateContent.Replace("[INVOICE_DATE]", details.Invoicedate.ToString("dd/MM/yyyy"));
            templateContent = templateContent.Replace("[TOTAL_TRUCK]", details.TotalTruckCount.ToString());
            templateContent = templateContent.Replace("[AMOUNT]", details.TotalInvoiceAmount.ToString());
            templateContent = templateContent.Replace("[AMOUNT_WORDS]", NumberHelper.NumberToWords((details.TotalInvoiceAmount - details.TotalDiscount).ToString()));
            templateContent = templateContent.Replace("[NET_AMOUNT]", (details.TotalInvoiceAmount - details.TotalDiscount).ToString());
            templateContent = templateContent.Replace("[DISCOUNT]", details.TotalDiscount.ToString());
            templateContent = templateContent.Replace("[ORGANIZATION_SHORTNAME]", details.OrganizationShortName);
            templateContent = templateContent.Replace("[ORGANIZATION_NAME]", details.OrganizationName);
            templateContent = templateContent.Replace("[ORGANIZATION_ADDRESS]", details.OrgnaizationAddress);
            templateContent = templateContent.Replace("[FROMDATE]", details.FromDate.ToString("dd/MM/yyyy"));
            templateContent = templateContent.Replace("[TODATE]", details.Todate.ToString("dd/MM/yyyy"));

            string folderPath = Server.MapPath("~/Documents/");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            string fileName = DateTime.Now.Ticks.ToString() + ".pdf";
            _htmlToPdf.ConvertHtmlToFile(templateContent, null, string.Concat(folderPath + "\\" + fileName));
            return fileName;
        }


        [HttpGet]
        public ActionResult InvoiceList()
        {
            try
            {
                UserDetailModel userdetail = (UserDetailModel)Session["UserDetails"];
                List<ProformaInvoice> list = WebAPIHelper.CallApi<List<ProformaInvoice>>(HttpMethods.Get, "GetInvoiceList", "Invoice", null, null, userdetail.UDID, userdetail.RoleId);
                ViewBag.InvoiceList = list;
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Something went wrong.";
                TempData["alertTitle"] = "Error";
                TempData["type"] = "error";
            }
            return View();
        }




        [HttpGet]
        public ActionResult DownloadPdf(int id)
        {
            try
            {
                string invoicePath = string.Empty;
                string folderPath = Server.MapPath("~/Documents/");
                ProformaInvoice details = WebAPIHelper.CallApi<ProformaInvoice>(HttpMethods.Get, "GetInvoiceDetails", "Invoice", null, id);
                if (details.InvoiceFilePath == string.Empty)
                {
                    ProformaInvoice model = new ProformaInvoice();
                    string path = GenerateInvoicePdf(Sql.ToInt(details.ProformaInvoiceId));
                    model.ProformaInvoiceId = (long)details.ProformaInvoiceId;
                    model.InvoiceFilePath = path;

                    ResponseInfo res = WebAPIHelper.CallApi<ResponseInfo>(HttpMethods.Post, "AddInvoicePdf", "Invoice", model);
                    invoicePath = path;
                }
                else
                {
                    invoicePath = details.InvoiceFilePath;
                }
                return File(Path.Combine(folderPath, invoicePath), "application/pdf", Server.UrlEncode(Path.Combine(folderPath, invoicePath)));
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Something went wrong.";
                TempData["alertTitle"] = "Error";
                TempData["type"] = "error";
                return RedirectToAction("InvoiceList");
            }
        }

        [HttpGet]
        public ActionResult ExportInvoice(int id)
        {
            DataTable dt = new DataTable();
            List<Model.Models.ProformaInvoiceTruckDetails> details = WebAPIHelper.CallApi<List<Model.Models.ProformaInvoiceTruckDetails>>(HttpMethods.Get, "GetProformaInvoiceTruckDetails", "Invoice", null, id);
            if (details.Count > 0)
            {
                dt = Sql.ToDataTable(details);
                dt.Columns.Remove("ProformaInvoiceDetId");
                dt.Columns.Remove("ProformaInvoiceId");
                dt.Columns.Remove("TruckCapacityId");
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt, "Invoices");

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=Invoice.xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
            return RedirectToAction("InvoiceList");
        }

        // Not used just for refrence
        //  public  async System.Threading.Tasks.Task<ContentResult> callAPI1()
        
        //{
        //    // string html = string.Empty;
        //    // // string url = ConfigurationManager.AppSettings["OrionAPI"] + Email + "";
        //    // //string urlAPI = ConfigurationManager.AppSettings["OrionAPI"];

        //    // //InvoiceDetailsAPIModel ObjApi = new InvoiceDetailsAPIModel();
        //    // //HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create(url);

        //    // var client = new RestClient(ConfigurationManager.AppSettings["OrionAPI"]);
        //    // // client.Authenticator = new HttpBasicAuthenticator(username, password);
        //    // var request = new RestRequest("LFTZ_APIS/orderDetailsv2.php?", Method.Post);
        //    // //request.AddHeader("Content-type", "application/json");
        //    // request.AddJsonBody(new 
        //    // { 
        //    //     CustomerCode= "ABCDr4",
        //    //     InvoiceAmount="5409" ,
        //    //     InvoiceReference= "test9"    
        //    // } );

        //    //     //request.AddStringBody("CustomerCode", "ABC");
        //    // //request.AddJsonBody("InvoiceAmount", "432432");
        //    // //request.AddJsonBody("InvoiceReference", "dasdsadsadsa");
        //    //request.AddParameter("apiKey", "MTgzOTMyVFVFU0RBWSAg");
        //    // request.AddParameter("companyID", "LFTZ");

        //    // //request.AddHeader("compid", "LFTZ");
        //    // request.AddHeader("Content-type", "application/json");
        //    // //request.AddHeader("Content-Length", "1000");
        //    // //request.AddHeader("Host", "");
        //    // //request.AddHeader("User-Agent", "");
        //    // //request.AddHeader("Accept", "*/*");
        //    // //request.AddHeader("Accept-Encoding", "gzip, deflate, br");
        //    //// request.AddHeader("Connection", "keep-alive");

        //    // request.AddHeader("compid", "LFTZ");
        //    // request.AddHeader("apikey", "MTgzOTMyVFVFU0RBWSAg");
        //    // //request.AddFile("file", path);
        //    // var response = client.Post(request);
        //    // var content = response.Content; // Raw content as string


        //    //    private static readonly HttpClient client = new HttpClient();

        //    //var values = new Dictionary<string, string>
        //    //    {
        //    //      { "thing1", "hello" },
        //    //      { "thing2", "world" }
        //    //};

        //    //    var content = new FormUrlEncodedContent(values);

        //    //    var response = await client.PostAsync(ConfigurationManager.AppSettings["OrionAPI"], content);

        //    //    var responseString = await response.Content.ReadAsStringAsync();

        //    using (var client = new HttpClient())
        //    {
        //        // Set headers
        //        client.DefaultRequestHeaders.Add("apikey", "MTgzOTMyVFVFU0RBWSAg");
        //        client.DefaultRequestHeaders.Add("compid", "LFTZ");

        //        // Set parameters
        //        var parameters = new Dictionary<string, string>
        //    {
        //        { "CustomerCode", "ABCD" },
        //        { "InvoiceAmount", "500" },
        //        { "InvoiceReference", "TEST22345" }

        //    };
        //        var content = new FormUrlEncodedContent(parameters);

        //        // Call API
        //        var response = await client.PostAsync("", content);

        //        // Get response body as string
        //        var responseBody = await response.Content.ReadAsStringAsync();
        //        Console.WriteLine(responseBody);
        //        return Content(responseBody);
        //    }


        //}
}
}