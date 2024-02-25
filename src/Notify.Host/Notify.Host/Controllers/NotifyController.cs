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

        ResponseDto _response;
        public NotifyController()
        {
            _response = new ResponseDto();
        }

        [HttpPost]
        public async Task<ResponseDto?> PostData([FromForm] RequestDto formData)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    throw new RequestException("Error: Model is invalid.");
                }

                string email = formData.Email;
                var file = formData.File;





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
