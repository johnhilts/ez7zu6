using System;

namespace ez7zu6.Member.Models
{
    public class ExperienceSaveModel
    {
        public int? ExperienceId { get; set; }
        public Guid UserId { get; set; }
        public string Notes { get; set; }
        public DateTime InputDateTime { get; set; }
        public DateTime Created { get; set; }
        public bool IsActive { get; set; }
    }
}