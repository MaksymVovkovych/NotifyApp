namespace Notify.Host.Models
{
    public class ResponseDto
    {
        public object? Result { get; set; }

        public string Message { get; set; } = null!;

        public bool IsSucces { get; set; } = true;
    }
}
