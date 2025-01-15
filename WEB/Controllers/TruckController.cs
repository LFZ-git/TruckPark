using Model.Models;
using Model.Models.ListOfValue;
using Model.Models.UserDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utility;
using WEB.APIHelper;
using System.Configuration;
using WEB.Helper;
using System.Data;
using ClosedXML.Excel;
using System.IO;

namespace WEB.Controllers
{
    public class TruckController : Controller
    {
        private void GetMasters()
        {
            UserDetailModel userdetail = (UserDetailModel)Session["UserDetails"];
            int transferId = (int)Model.CommonEnum.LOV.LOVId.TransferType;
            List<ListOfValueModel> TransferTypeList = WebAPIHelper.CallApi<List<ListOfValueModel>>(HttpMethods.Get, "GetListOfValue", "ListOfValue", null, transferId);
            ViewBag.TransferType = new SelectList(TransferTypeList, "LOVId", "LOVName");

            List<M_TruckCapacity> CategoryList = WebAPIHelper.CallApi<List<M_TruckCapacity>>(HttpMethods.Get, "GetTruckCapacity", "Master", null, null);
            ViewBag.TruckCapacityList = new SelectList(CategoryList, "TruckCapacityId", "TruckCapacity");

            int materialTypeId = (int)Model.CommonEnum.LOV.LOVId.MaterialType;
            List<ListOfValueModel> MaterialTypeList = WebAPIHelper.CallApi<List<ListOfValueModel>>(HttpMethods.Get, "GetListOfValue", "ListOfValue", null, materialTypeId);
            ViewBag.MaterialType = new SelectList(MaterialTypeList, "LOVId", "LOVName");

            if (userdetail.RoleId == (int)Model.CommonEnum.LOV.Role.LFZSecurity || userdetail.RoleId == (int)Model.CommonEnum.LOV.Role.LFZAdmin)
            {
                List<OrganizationModel> organizationList = WebAPIHelper.CallApi<List<OrganizationModel>>(HttpMethods.Get, "GetOrganizationList", "Organization");
                ViewBag.OrganizationList = new SelectList(organizationList, "OrganizationID", "CompanyShortName");
            }

            if (userdetail.RoleId == (int)Model.CommonEnum.LOV.Role.EnterpriseGroupAdmin)
            {
                List<OrganizationModel> organizationGroupList = WebAPIHelper.CallApi<List<OrganizationModel>>(HttpMethods.Get, "GetOrganizationGroupList", "Organization", null, null, userdetail.UDID);
                ViewBag.OrganizationList = new SelectList(organizationGroupList, "OrganizationID", "CompanyShortName");
            }

            if (userdetail.RoleId == (int)Model.CommonEnum.LOV.Role.EnterpriseAdmin)
            {
                OrganizationModel organizationDetails = WebAPIHelper.CallApi<OrganizationModel>(HttpMethods.Get, "GetOrganizationDetails", "Organization", null, userdetail.OrganizationID);
                ViewBag.organizationName = organizationDetails.CompanyShortName;

            }

            List<Truck> list = WebAPIHelper.CallApi<List<Truck>>(HttpMethods.Get, "GetTruckInfo", "Truck");
            ViewBag.TruckList = list.Select(v => new SelectListItem
            {
                Value = v.TruckNo,
                Text = v.TruckNo
            }).Cast<object>().ToArray();

            AddTruckPark model = new AddTruckPark();
            if (userdetail.RoleId == (int)Model.CommonEnum.LOV.Role.EnterpriseAdmin)
            {
                model.OwnedByOrganizationId = userdetail.OrganizationID;
            }
        }

        [HttpGet]
        public ActionResult Add()
        {
            GetMasters();
            return View();
        }

        [HttpPost]
        public ActionResult Add(AddTruckPark objAddTruckPark)
        {
            //if (ModelState.IsValid)
            //{
            Truck truckmodel = new Truck();
            truckmodel.TruckNo = objAddTruckPark.TruckNo;
            TruckStatus truckList = WebAPIHelper.CallApi<TruckStatus>(HttpMethods.Post, "GetTruckDetails", "Truck", truckmodel);
            if (truckList.IsNotNull())
            {
                if (truckList.IsCheckedIn == true || truckList.IsForecasted == true)
                {
                    if (truckList.IsCheckedIn)
                    {
                        TempData["msg"] = "Provided truck is already checked in!";
                        TempData["alertTitle"] = "Found!";
                        TempData["type"] = "error";
                    }
                    if (truckList.IsForecasted)
                    {
                        TempData["msg"] = "Provided truck is already forecasted!";
                        TempData["alertTitle"] = "Found!";
                        TempData["type"] = "error";
                    }

                    return RedirectToAction("Dashboard", "Home");
                }
                else
                {
                    try
                    {
                        AddTruckDetails(objAddTruckPark);

                        TempData["msg"] = "Truck Added Successfully.";
                        TempData["alertTitle"] = "Success";
                        TempData["type"] = "success";
                    }
                    catch (Exception ex)
                    {
                        TempData["msg"] = "Something went wrong.";
                        TempData["alertTitle"] = "Error";
                        TempData["type"] = "error";
                    }
                    GetMasters();
                    return View();
                }
            }
            else
            {
                try
                {
                    AddTruckDetails(objAddTruckPark);

                    TempData["msg"] = "Truck Added Successfully.";
                    TempData["alertTitle"] = "Success";
                    TempData["type"] = "success";
                }
                catch (Exception ex)
                {
                    TempData["msg"] = "Something went wrong.";
                    TempData["alertTitle"] = "Error";
                    TempData["type"] = "error";
                }
                GetMasters();
                return View();
            }

        }

        public void AddTruckDetails(AddTruckPark objAddTruckPark)
        {
            UserDetailModel userdetail = (UserDetailModel)Session["UserDetails"];
            objAddTruckPark.Createdby = userdetail.UDID;
            //objAddTruckPark.OwnedByOrganizationId = userdetail.OrganizationID;
            var nigeria = TimeZoneInfo.FindSystemTimeZoneById("W. Central Africa Standard Time");
            var nigerianTime = TimeZoneInfo.ConvertTime(DateTime.UtcNow, nigeria);
            if (userdetail.RoleId == (int)Model.CommonEnum.LOV.Role.EnterpriseAdmin || userdetail.RoleId == (int)Model.CommonEnum.LOV.Role.EnterpriseGroupAdmin)
            {
                objAddTruckPark.OwnedByOrganizationId = userdetail.OrganizationID;
                objAddTruckPark.CalledByOrganizationId = userdetail.OrganizationID;
                objAddTruckPark.IsForecasted = true;
                objAddTruckPark.IsCheckedIn = false;
                objAddTruckPark.ActualArrivalDate = nigerianTime;
            }
            if (userdetail.RoleId == (int)Model.CommonEnum.LOV.Role.LFZSecurity)
            {
                objAddTruckPark.IsForecasted = false;
                objAddTruckPark.IsCheckedIn = true;
                objAddTruckPark.ActualArrivalDate = nigerianTime;
            }

            ResponseInfo response = WebAPIHelper.CallApi<ResponseInfo>(HttpMethods.Post, "AddTruck", "Truck", objAddTruckPark);

            if (response.IsSuccess)
            {
                if (userdetail.RoleId == (int)Model.CommonEnum.LOV.Role.LFZSecurity)
                {
                    UpdateTruckStatus TruckListmodel = new UpdateTruckStatus();
                    TruckListmodel.TruckDetailsList = Sql.ToString(response.ID);
                    List<SendMailModel> detailsList = WebAPIHelper.CallApi<List<SendMailModel>>(HttpMethods.Post, "SendMailDetails", "Truck", TruckListmodel);

                    foreach (var item in detailsList)
                    {
                        OrganizationModel model = WebAPIHelper.CallApi<OrganizationModel>(HttpMethods.Get, "GetOrganizationDetails", "Organization", null, objAddTruckPark.CalledByOrganizationId);
                        //string ccEmail = ConfigurationManager.AppSettings["ccEmailNikhil"];

                        string emailSubject = string.Empty;
                        string emailBody = string.Empty;
                        Email email = new Email();

                        emailSubject = UpdatedText(Model.CommonEnum.EmailSubject.CheckIn, objAddTruckPark.TruckNo, model.CompanyShortName, Sql.ToDateTime(objAddTruckPark.ActualArrivalDate).ToString("dd-MMM-yyyy hh:mm::ss"));
                        emailBody = UpdatedText(Model.CommonEnum.EmailBody.CheckIn, objAddTruckPark.TruckNo, model.CompanyShortName, Sql.ToDateTime(objAddTruckPark.ActualArrivalDate).ToString("dd-MMM-yyyy hh:mm::ss"));
                        var result = email.SendMail(item.EmailId, emailSubject, emailBody);
                    }
                }
                //else if (userdetail.RoleId == (int)Model.CommonEnum.LOV.Role.EnterpriseAdmin || userdetail.RoleId == (int)Model.CommonEnum.LOV.Role.EnterpriseGroupAdmin)
                //{
                //    UpdateTruckStatus TruckListmodel = new UpdateTruckStatus();
                //    TruckListmodel.TruckDetailsList = Sql.ToString(response.ID);
                //    List<SendMailModel> detailsList = WebAPIHelper.CallApi<List<SendMailModel>>(HttpMethods.Post, "SendMailDetails", "Truck", TruckListmodel);

                //    foreach (var item in detailsList)
                //    {
                //        OrganizationModel model = WebAPIHelper.CallApi<OrganizationModel>(HttpMethods.Get, "GetOrganizationDetails", "Organization", null, objAddTruckPark.CalledByOrganizationId);
                //       // string ccEmail = ConfigurationManager.AppSettings["ccEmailNikhil"];
                //        string LFZHrEmail = ConfigurationManager.AppSettings["LFZHrEmail"];

                //        string emailSubject = string.Empty;
                //        string emailBody = string.Empty;
                //        Email email = new Email();

                //        emailSubject = UpdatedText(Model.CommonEnum.EmailSubject.Added, objAddTruckPark.TruckNo, model.CompanyShortName, Sql.ToDateTime(objAddTruckPark.ActualArrivalDate).ToString("dd-MMM-yyyy hh:mm::ss"));
                //        emailBody = UpdatedText(Model.CommonEnum.EmailBody.Added, objAddTruckPark.TruckNo, model.CompanyShortName, Sql.ToDateTime(objAddTruckPark.ActualArrivalDate).ToString("dd-MMM-yyyy hh:mm::ss"));
                //        var result = email.SendMail(LFZHrEmail, emailSubject, emailBody);
                //    }
                //}

            }
        }
        [HttpGet]
        public ActionResult TruckList()
        {
            try
            {
                UserDetailModel userdetail = (UserDetailModel)Session["UserDetails"];
                List<TruckDetails> list = WebAPIHelper.CallApi<List<TruckDetails>>(HttpMethods.Get, "GetTruckList", "Truck", null, userdetail.OrganizationID, userdetail.UDID, userdetail.RoleId);
                ViewBag.TruckList = list.Where(a => a.IsCalledOut == false).ToList();
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
        public JsonResult GetTrucks(string Prefix)
        {
            Truck model = new Truck();
            model.TruckNo = Prefix;
            List<Truck> truckList = WebAPIHelper.CallApi<List<Truck>>(HttpMethods.Post, "GetTruckInfo", "Truck", model);
            return Json(truckList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CheckTruckStatus(string Prefix)
        {
            Truck model = new Truck();
            model.TruckNo = Prefix;
            TruckStatus truckList = WebAPIHelper.CallApi<TruckStatus>(HttpMethods.Post, "GetTruckDetails", "Truck", model);
            return Json(truckList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateStatus(FormCollection formCollection)
        {
            try
            {
                string Command = formCollection["Command"];
                UserDetailModel userdetail = (UserDetailModel)Session["UserDetails"];
                UpdateTruckStatus model = new UpdateTruckStatus();
                if (Command == "callout")
                {
                    model.status = (int)Model.CommonEnum.LOV.TruckOperations.CALLEDOUT;
                }
                if (Command == "delete")
                {
                    model.status = (int)Model.CommonEnum.LOV.SqlOperations.DELETE;
                }
                if (Command == "checkout")
                {
                    model.status = (int)Model.CommonEnum.LOV.TruckOperations.CHECKOUT;
                }
                if (Command == "checkin")
                {
                    model.status = (int)Model.CommonEnum.LOV.TruckOperations.CHECKIN;
                }


                model.TruckDetailsList = formCollection["TruckDetailsId"];
                model.ModifiedBy = userdetail.UDID;
                model.RoleId = userdetail.RoleId;

                ResponseInfo response = WebAPIHelper.CallApi<ResponseInfo>(HttpMethods.Post, "UpdateTruckStatus", "Truck", model);

                if (response.IsSuccess)
                {
                    string ccEmail = ConfigurationManager.AppSettings["ccEmailNikhil"];

                    string emailSubject = string.Empty;
                    string emailBody = string.Empty;
                    Email email = new Email();

                    List<SendMailModel> detailsList = WebAPIHelper.CallApi<List<SendMailModel>>(HttpMethods.Post, "SendMailDetails", "Truck", model);
                    if (Command == "checkout")
                    {
                        foreach (var item in detailsList)
                        {
                            emailSubject = UpdatedText(Model.CommonEnum.EmailSubject.CheckOut, item.TruckNo, item.CompanyShortName, item.ActualDepartureDate.ToString("dd-MMM-yyyy hh:mm::ss"));
                            emailBody = UpdatedText(Model.CommonEnum.EmailBody.CheckOut, item.TruckNo, item.CompanyShortName, item.ActualDepartureDate.ToString("dd-MMM-yyyy hh:mm::ss"));
                            var result=email.SendMail(item.EmailId, emailSubject, emailBody,ccEmail);
                        }


                        TempData["msg"] = "Truck Checked Out Successfully.";
                        TempData["alertTitle"] = "Success";
                        TempData["type"] = "success";
                        return RedirectToAction("TruckCheckedInList");
                    }
                    else if (Command == "checkin")
                    {
                        foreach (var item in detailsList)
                        {
                            emailSubject = UpdatedText(Model.CommonEnum.EmailSubject.CheckIn, item.TruckNo, item.CompanyShortName, item.ActualArrivalDate.ToString("dd-MMM-yyyy hh:mm::ss"));
                            emailBody = UpdatedText(Model.CommonEnum.EmailBody.CheckIn, item.TruckNo, item.CompanyShortName, item.ActualArrivalDate.ToString("dd-MMM-yyyy hh:mm::ss"));
                            var result = email.SendMail(item.EmailId, emailSubject, emailBody,ccEmail);
                        }


                        TempData["msg"] = "Truck Checked In Successfully.";
                        TempData["alertTitle"] = "Success";
                        TempData["type"] = "success";
                        return RedirectToAction("TruckList");
                    }
                    else if (Command == "callout")
                    {
                        TempData["msg"] = "Truck Called Out Successfully.";
                        TempData["alertTitle"] = "Success";
                        TempData["type"] = "success";
                        return RedirectToAction("TruckList");
                    }
                    else
                    {
                        TempData["msg"] = "Truck Deleted Successfully.";
                        TempData["alertTitle"] = "Success";
                        TempData["type"] = "success";
                        return RedirectToAction("TruckList");
                    }
                }
                else
                {
                    return RedirectToAction("TruckList");
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Something went wrong.";
                TempData["alertTitle"] = "Error";
                TempData["type"] = "error";
                return RedirectToAction("TruckList");
            }
        }

        [HttpGet]
        public ActionResult CalledOutVehicles()
        {
            try
            {
                UserDetailModel userdetail = (UserDetailModel)Session["UserDetails"];
                List<TruckDetails> list = WebAPIHelper.CallApi<List<TruckDetails>>(HttpMethods.Get, "GetCalledOutTruckList", "Truck", null, userdetail.OrganizationID, userdetail.UDID, userdetail.RoleId);

                ViewBag.TruckList = list;
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
        public ActionResult TruckCheckedInList()
        {
            try
            {
                UserDetailModel userdetail = (UserDetailModel)Session["UserDetails"];
                List<TruckDetails> list = WebAPIHelper.CallApi<List<TruckDetails>>(HttpMethods.Get, "GetTruckCheckedInList", "Truck", null, userdetail.OrganizationID, userdetail.UDID, userdetail.RoleId);
                ViewBag.TruckList = list;
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
        public ActionResult TruckCheckedoutList()
        {
            try
            {
                UserDetailModel userdetail = (UserDetailModel)Session["UserDetails"];
                List<TruckDetails> list = WebAPIHelper.CallApi<List<TruckDetails>>(HttpMethods.Get, "GetTruckCheckedOutList", "Truck", null, userdetail.OrganizationID, userdetail.UDID, userdetail.RoleId);
                ViewBag.TruckList = list;
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
        public ActionResult BillableTrucks()
        {
            try
            {
                List<TruckDetails> list = WebAPIHelper.CallApi<List<TruckDetails>>(HttpMethods.Get, "GetBillableTrucks", "Truck");
                ViewBag.TruckList = list;
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
        public ActionResult Edit(int id)
        {
            try
            {
                GetMasters();
                EditTruckDetails model = WebAPIHelper.CallApi<EditTruckDetails>(HttpMethods.Get, "GetTruckDetailsOnId", "Truck", null, id);
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Something went wrong.";
                TempData["alertTitle"] = "Error";
                TempData["type"] = "error";
                return View();
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, EditTruckDetails model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UserDetailModel userdetail = (UserDetailModel)Session["UserDetails"];
                    model.Createdby = userdetail.UDID;
                    model.TruckDetailsId = id;

                    ResponseInfo response = WebAPIHelper.CallApi<ResponseInfo>(HttpMethods.Post, "UpdateTruck", "Truck", model);

                    TempData["msg"] = "Truck Updated Successfully.";
                    TempData["alertTitle"] = "Success";
                    TempData["type"] = "success";
                }
                catch (Exception ex)
                {
                    TempData["msg"] = "Something went wrong.";
                    TempData["alertTitle"] = "Error";
                    TempData["type"] = "error";
                }
            }

            return RedirectToAction("Edit", new { id = id });
        }
        public string UpdatedText(string text, string truckNo, string companyName, string date)
        {
            var replacements = new Dictionary<string, string>
                                                {
                                                    { "[TruckNo]", truckNo},
                                                    { "[CompanyShortName]", companyName},
                                                    { "[ActualArrivalDate]", date},
                                                };

            foreach (var replacement in replacements)
            {
                text = text.Replace(replacement.Key, replacement.Value);
            }

            return text;
        }


        [HttpGet]
        public ActionResult ExportExcel(string page)
        {
            try
            {
                List<TruckDetails> list = (List<TruckDetails>)TempData["Data"];
                if (list.Count > 0)
                {
                    DataTable dt = Sql.ToDataTable(list);
                    dt.Columns.Remove("TruckDetailsId");
                    dt.Columns.Remove("TruckId");
                    dt.Columns.Remove("CalledByOrganizationId");
                    dt.Columns.Remove("TruckCapacityId");
                    dt.Columns.Remove("LocalTransferTypeId");
                    dt.Columns.Remove("IsForecasted");
                    dt.Columns.Remove("IsCheckedIn");
                    dt.Columns.Remove("IsCalledOut");
                    dt.Columns.Remove("Createdby");
                    dt.Columns.Remove("IsActive");
                    dt.Columns.Remove("TruckCapacity");
                    dt.Columns.Remove("IsBilled");
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt, "Truck");

                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;filename=TruckList.xlsx");
                        using (MemoryStream MyMemoryStream = new MemoryStream())
                        {
                            wb.SaveAs(MyMemoryStream);
                            MyMemoryStream.WriteTo(Response.OutputStream);
                            Response.Flush();
                            Response.End();
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                return RedirectToAction(page);
            }

            return RedirectToAction(page);
        }

        [HttpGet]
        public ActionResult ExportExcel1(string page)
        {
            try
            {
                int OrganizationID = -1;
                UserDetailModel userdetail = (UserDetailModel)Session["UserDetails"];

                List<TruckDetails> list = WebAPIHelper.CallApi<List<TruckDetails>>(HttpMethods.Get, "GetTruckCheckedOutList", "Truck", null, OrganizationID, userdetail.UDID, userdetail.RoleId);
                ViewBag.TruckList = list;

                if (list.Count > 0)
                {
                    DataTable dt = Sql.ToDataTable(list);
                    dt.Columns.Remove("TruckDetailsId");
                    dt.Columns.Remove("TruckId");
                    dt.Columns.Remove("CalledByOrganizationId");
                    dt.Columns.Remove("TruckCapacityId");
                    dt.Columns.Remove("LocalTransferTypeId");
                    dt.Columns.Remove("IsForecasted");
                    dt.Columns.Remove("IsCheckedIn");
                    dt.Columns.Remove("IsCalledOut");
                    dt.Columns.Remove("Createdby");
                    dt.Columns.Remove("IsActive");
                    dt.Columns.Remove("TruckCapacity");
                    dt.Columns.Remove("IsBilled");
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt, "Truck");

                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;filename=TruckCheckedOutList.xlsx");
                        using (MemoryStream MyMemoryStream = new MemoryStream())
                        {
                            wb.SaveAs(MyMemoryStream);
                            MyMemoryStream.WriteTo(Response.OutputStream);
                            Response.Flush();
                            Response.End();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction(page);
            }

            return RedirectToAction(page);
        }

        public ActionResult ViewExpectedTrucksEXT()
        {

            List<ViewTrucksEXT> lst = WebAPIHelper.CallApi<List<ViewTrucksEXT>>(HttpMethods.Get, "ViewTruckExtList", "Truck");
            return View("ViewTrucksEXT", lst);
        }
    }
}