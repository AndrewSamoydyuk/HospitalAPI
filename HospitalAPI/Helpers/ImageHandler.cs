using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Web;
using System.Net.Http;

namespace HospitalAPI.Helpers
{
    public class ImageHandler
    {
        public static async Task<string> UploadImage(HttpContent file)
        {
            string root = HttpContext.Current.Server.MapPath("~/Content/Images/");

            var filename = file.Headers.ContentDisposition.FileName.Trim('\"');
            byte[] fileArray = await file.ReadAsByteArrayAsync();

            using (System.IO.FileStream fs = new System.IO.FileStream(root + filename, System.IO.FileMode.Create))
            {
                await fs.WriteAsync(fileArray, 0, fileArray.Length);
            }

            return"~/Content/Images/" + filename;
            
        }

        public static void DeleteImageIfExist(string imageUri)
        {
            if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(imageUri)))
            {
                System.IO.File.Delete(HttpContext.Current.Server.MapPath(imageUri));
            }
        }
    }
}