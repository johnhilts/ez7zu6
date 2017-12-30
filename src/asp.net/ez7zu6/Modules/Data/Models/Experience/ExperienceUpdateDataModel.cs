using System;

namespace ez7zu6.Data.Models.Experience
{
    public class ExperienceUpdateDataModel
    {
        public Guid ExperienceId { get; set; }
        public Guid UserId { get; set; }
        public string Notes { get; set; }
        public DateTime InputDateTime { get; set; }
    }
}