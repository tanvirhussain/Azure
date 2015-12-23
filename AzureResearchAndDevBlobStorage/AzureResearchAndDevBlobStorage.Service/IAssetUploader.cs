using System;

namespace AzureResearchAndDevBlobStorage.Service
{
    public interface IAssetUploader
    {
        string Upload(string storageAccount, string blobName, System.IO.Stream blobToUpload);
    }
}
