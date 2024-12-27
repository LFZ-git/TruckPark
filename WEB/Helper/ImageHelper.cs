using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace WEB.Helper
{
    public class ImageHelper
    {

        public string SaveImage(HttpPostedFileBase FileName)
        {
            string imagePath = ConfigurationManager.AppSettings["IncidentfolderName"].ToString();
            string fileurl = ConfigurationManager.AppSettings["IncidentDocPath"].ToString();

           // string folderPath = Server.MapPath("~/Documents/");
            string path = Path.Combine(fileurl, imagePath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            
            var fileName = Guid.NewGuid().ToString() + ".jpg";
            FileName.SaveAs(Path.Combine(path, fileName));
            path = Path.Combine(imagePath, fileName);
            return path;
        }

        public string DeleteDoc(string filePath)
        {
            string fileurl = ConfigurationManager.AppSettings["IncidentDocPath"].ToString();

            var dirPath = Path.Combine(
                        fileurl, filePath);
            if (System.IO.File.Exists(dirPath))
            {
                //Directory.Delete(dirPath, true);
                System.IO.File.Delete(dirPath);
            }

            return dirPath;
        }
    }
}