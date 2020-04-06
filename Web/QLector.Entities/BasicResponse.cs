namespace QLector.Entities
{
    public class BasicResponse
    {
        public bool IsSuccess { get; }
        public string Message { get; }

        public BasicResponse(bool isSuccess, string message = null)
        {
            IsSuccess = isSuccess;
            Message = message;
        }
    }
}
