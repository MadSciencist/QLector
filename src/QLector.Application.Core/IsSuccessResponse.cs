namespace QLector.Application.Core
{
    public class IsSuccessResponse
    {
        public IsSuccessResponse(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public bool IsSuccess { get; }
    }
}
