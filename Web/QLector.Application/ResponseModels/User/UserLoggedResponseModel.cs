using System;

namespace QLector.Application.ResponseModels.User
{
    public class UserLoggedResponseModel
    {
        public long Id { get; set; }
        public string Token { get; set; }
        public DateTime IssuedAt { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
