namespace HotelsApplication.Application.ActionResults
{
    public class ToggleStatusResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public ToggleStatusResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }

}
