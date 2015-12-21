using AzureResearchAndDevBlobStorage.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AzureResearchAndDevBlobStorage.Web.Controllers
{
    public class UploadImageController : Controller
    {
        private readonly IAssetUploader _assetUploader;

        public UploadImageController(IAssetUploader assetUploader) 
        {
            _assetUploader = assetUploader;
        }

        //
        // GET: /UploadImage/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public void Index(HttpPostedFileBase file) 
        {
            if (file != null && file.ContentLength > 0)
            {
                // extract only the filename
                var fileName = Path.GetFileName(file.FileName);

                var uploadedUrl = _assetUploader.Upload(ConfigurationManager.AppSettings["StorageConnectionString"], fileName, file.InputStream);
            }       
        }
	}
}