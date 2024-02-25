using System.ComponentModel.DataAnnotations;

namespace Notify.Host.Models
{
    public class RequestDto
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "File is required.")]
        [DataType(DataType.Upload)]
        [FileExtensions(Extensions = ".docx", ErrorMessage = "Invalid file format. Only .docx files are allowed.")]
        public Microsoft.AspNetCore.Http.IFormFile File { get; set; }
    }
}
