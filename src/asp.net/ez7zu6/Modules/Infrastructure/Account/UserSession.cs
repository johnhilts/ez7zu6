using System;

namespace ez7zu6.Infrastructure.Account
{
    public class UserSession
    {
        public Guid SessionId { get; set; }
        public Guid UserId { get; set; }
        public bool IsAnonymous { get; set; }
    }
}