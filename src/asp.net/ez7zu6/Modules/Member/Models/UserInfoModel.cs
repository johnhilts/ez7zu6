using System;

namespace ez7zu6.Member.Models
{
    public class UserInfoModel
    {
        public Guid? UserId { get; set; }
        public string Username { get; set; }
        public bool CanAuthenticate { get; set; }
        public bool CanRegister { get; set; }
        public string Message { get; set; }
    }
}
