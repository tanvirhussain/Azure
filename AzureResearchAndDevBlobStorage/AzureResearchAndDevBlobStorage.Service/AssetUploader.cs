using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace AzureResearchAndDevBlobStorage.Service
{
    public class AssetUploader : IAssetUploader
    {
        const string containerName = "VeconfigImageContainer";

        public string Upload(string storageAccount, string blobName, System.IO.Stream blobToUpload)
        {
            var cloudBlobClient = ConnectAzure(storageAccount);

            // Retrieve a reference to a container.
            var container = cloudBlobClient.GetContainerReference(containerName);

            // Create the container if it doesn't already exist.
            container.CreateIfNotExists();

            var blockBlob = container.GetBlockBlobReference(blobName);

            blockBlob.UploadFromStream(blobToUpload);

            var accessUrl = blockBlob.Uri.ToString();

            return accessUrl;
        }

        private CloudBlobClient ConnectAzure(string storageAccount) 
        {
            return CloudStorageAccount.Parse(storageAccount).CreateCloudBlobClient();
        }    
    }
}
