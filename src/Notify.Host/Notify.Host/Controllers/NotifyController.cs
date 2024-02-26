using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Expressions;
using Notify.Host.Exceptions;
using Notify.Host.Models;

namespace Notify.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotifyController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        ResponseDto _response;
        public NotifyController(IConfiguration configuration)
        {
            _response = new ResponseDto();
            _configuration = configuration;

        }

        [HttpPost]
        public async Task<ResponseDto?> PostData([FromForm] RequestDto formData)
        {
            try
            {

                if (!formData.IsValid())
                {
                    throw new RequestException("Error: Model is invalid.");
                }

                string email = formData.Email;
                var file = formData.File;

                var connectionString = _configuration["BlobStorageSettings:ConnectionString"];
                var containerName = _configuration["BlobStorageSettings:ContainerName"];
                var blobServiceClient = new BlobServiceClient(connectionString);
                var blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);


                var fileName = formData.File.FileName;
                var blobClient = blobContainerClient.GetBlobClient(fileName);
                using (var fileStream = formData.File.OpenReadStream())
                {
                    await blobClient.UploadAsync(fileStream, true);
                }

                return _response;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSucces = false;
            }

            return _response;

        }
    }
}
