using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GlobalAzure19.Entities;
using GlobalAzure19.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace GlobalAzure19.Services
{
    public class BlobFileService : IBlobFile
    {
        public async Task<string> UploadBlobFileAsync(IFormFile file)
        {
            var storageAccount = CloudStorageAccount.Parse(Environment.GetEnvironmentVariable("gabootcamp_STORAGE"));
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("process");
            await container.CreateIfNotExistsAsync();
            var blockBlob = container.GetBlockBlobReference(file.FileName);

            using (var fileStream = file.OpenReadStream())
            {
                await blockBlob.UploadFromStreamAsync(fileStream);
            }

            return blockBlob.Uri.ToString();
        }

        public async Task<List<BlobFile>> GetBlob()
        {
            var storageAccount = CloudStorageAccount.Parse(Environment.GetEnvironmentVariable("gabootcamp_STORAGE"));
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("processed");
            var blobs = new List<BlobFile>();

            BlobContinuationToken blobContinuationToken = null;
            do
            {
                var results = await container.ListBlobsSegmentedAsync(null, blobContinuationToken);
                blobContinuationToken = results.ContinuationToken;
                foreach (IListBlobItem item in results.Results)
                {
                    blobs.Add(new BlobFile { Name = item.Uri.Segments.GetValue(item.Uri.Segments.Length -1).ToString() , BlolbUrl = item.Uri.ToString() });
                }
            } while (blobContinuationToken != null);

            return blobs;
        }
    }
}
