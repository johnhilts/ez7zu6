using System;

namespace ez7zu6.Data.Models.Account
{
    public class AccountQueryModel
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool NoMatch { get; set; }
    }
}
