using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GlobalAzure19.Entities;
using Microsoft.AspNetCore.Http;

namespace GlobalAzure19.Interfaces
{
    public interface IBlobFile
    {
        Task<string> UploadBlobFileAsync(IFormFile file);
        Task<List<BlobFile>> GetBlob();
    }
}
