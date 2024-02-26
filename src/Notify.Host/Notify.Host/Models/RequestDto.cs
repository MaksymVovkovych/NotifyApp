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
        public IFormFile File { get; set; }

        public bool IsValid()
        {
            if (string.IsNullOrEmpty(Email) || File == null)
                return false;

            // Перевірка типу файлу
            var fileExtension = Path.GetExtension(File.FileName);
            if (fileExtension != ".docx")
                return false;

            // Перевірка MIME-типу файлу (додатковий захист)
            if (File.ContentType != "application/vnd.openxmlformats-officedocument.wordprocessingml.document")
                return false;

            return true;
        }
    }
}
