using System.Collections.Generic;
using System.Threading.Tasks;
using GlobalAzure19.Entities;
using GlobalAzure19.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalAzure19.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlobController : ControllerBase
    {
        private readonly IBlobFile _blobFile;

        public BlobController(IBlobFile blobFile)
        {
            _blobFile = blobFile;
        }

        [HttpPost]
        public async Task<IActionResult> Post(string name, string email, IFormFile file)
        {
            var url = await _blobFile.UploadBlobFileAsync(file);
            return Redirect("../index.html");
        }

        [HttpGet]
        public async Task<List<BlobFile>> Get()
        {
            var blob = await _blobFile.GetBlob();
            return blob;
        }
    }
}
