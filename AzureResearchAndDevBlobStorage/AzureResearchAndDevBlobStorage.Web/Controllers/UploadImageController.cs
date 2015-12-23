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
      
        [HttpPost]
        public JsonResult Index() 
        {
            var uploadResponse = new Models.UploadResponse();     

            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0] as HttpPostedFileBase;
                
                var fileName = Path.GetFileName(file.FileName);

                var uploadedUrl = _assetUploader.Upload(ConfigurationManager.AppSettings["StorageConnectionString"], fileName, file.InputStream);

                uploadResponse.Message = string.Format("Image has been uploaded to the following location : {0}", uploadedUrl);
                uploadResponse.UploadStatus = (int)Enums.UploadStatus.Uploaded;
            }
            else 
            {
                uploadResponse.Message = "Please attach a file";
                uploadResponse.UploadStatus = (int)Enums.UploadStatus.NoFile;
            }

            return Json(uploadResponse, JsonRequestBehavior.AllowGet);
        }
	}
}