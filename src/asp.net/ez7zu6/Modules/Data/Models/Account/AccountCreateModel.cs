using System;

namespace ez7zu6.Data.Models.Account
{
    public class AccountCreateModel
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public byte[] UserPassword { get; set; }
        public bool IsAnonymous { get; set; }
        public DateTime OptedIn { get; set; }
    }
}
