using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ArticlProgect.Code
{
    public class FilesHelper
    {
        private readonly IWebHostEnvironment webHost;

        public FilesHelper(IWebHostEnvironment webHost)
        {
            this.webHost = webHost;
        }
         
        

        //UploadFiles

        public string UploadFile(IFormFile file,string folder)
        {
            if(file !=null)
            {
                var fileDir = Path.Combine(webHost.ContentRootPath, folder);
                var fileName = Guid.NewGuid() + "-" + file.FileName;
                var filepath = Path.Combine(fileDir, fileName);

                using(FileStream fileStream=new FileStream(filepath,FileMode.Create))
                {
                    file.CopyTo(fileStream);
                    return fileName;
                }


            }
            else
            {
                return string.Empty;
            }
        }
    }
}
