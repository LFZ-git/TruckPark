using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WEB.Helper
{
    public class ExtAPIHelper
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        private static void AddAuthHeaders(HttpRequestMessage request)
        {
            request.Headers.Add("X-External-App-Key", ConfigurationManager.AppSettings["EXT_CallUp_APPKey"]);
        }

        public static (HttpStatusCode, TResponse) Post<TRequest, TResponse>(string url, TRequest body)
        {
            try
            {
                var jsonBody = JsonConvert.SerializeObject(body);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                var request = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = content
                };

                AddAuthHeaders(request);

                var response = _httpClient.SendAsync(request).GetAwaiter().GetResult();
                var statusCode = response.StatusCode;

                var responseString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                var result = JsonConvert.DeserializeObject<TResponse>(responseString);
                return (statusCode, result);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in POST request: {ex.Message}");
            }
        }

        public static (HttpStatusCode, TResponse) Get<TResponse>(string url)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);

                AddAuthHeaders(request);

                var response = _httpClient.SendAsync(request).GetAwaiter().GetResult();
                var statusCode = response.StatusCode;

                var responseString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                var result = JsonConvert.DeserializeObject<TResponse>(responseString);
                return (statusCode, result);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in GET request: {ex.Message}");
            }
        }

        public static (HttpStatusCode, TResponse) PostModelWithFiles<TResponse>(string url, object model)
        {
            try
            {
                using (var multipartContent = new MultipartFormDataContent())
                {
                    // Iterate through the properties of the model and add them as form data
                    var modelProperties = model.GetType().GetProperties();

                    foreach (var property in modelProperties)
                    {
                        if (property.PropertyType == typeof(List<HttpPostedFileBase>))
                        {
                            // Handle file list property
                            var files = (List<HttpPostedFileBase>)property.GetValue(model);

                            foreach (var file in files)
                            {
                                if (file != null && file.ContentLength > 0)
                                {
                                    var fileContent = new StreamContent(file.InputStream);
                                    // Use the property name as the key for the file
                                    multipartContent.Add(fileContent, property.Name, file.FileName);
                                }
                            }
                        }
                        else
                        {
                            // Add regular model properties to the form data
                            var value = property.GetValue(model);
                            if (value != null)
                            {
                                var stringValue = value.ToString();
                                multipartContent.Add(new StringContent(stringValue, Encoding.UTF8), property.Name);
                            }
                        }
                    }

                    var request = new HttpRequestMessage(HttpMethod.Post, url)
                    {
                        Content = multipartContent
                    };

                    // Add default and custom headers
                    AddAuthHeaders(request);

                    // Send the request and get the response
                    var response = _httpClient.SendAsync(request).GetAwaiter().GetResult();
                    var statusCode = response.StatusCode;

                    var responseString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    var result = JsonConvert.DeserializeObject<TResponse>(responseString);
                    return (statusCode, result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in POST request: {ex.Message}");
            }
        }
    }
}
