using System;

namespace ez7zu6.Member.Models
{
    public class ExperienceQueryModel
    {
        // TODO: make EID an GUID
        public int? ExperienceId { get; set; }
        public string Notes { get; set; }
        public DateTime InputDateTime { get; set; }
    }
}