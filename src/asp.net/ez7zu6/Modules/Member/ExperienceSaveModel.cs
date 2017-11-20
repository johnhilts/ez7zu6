using System;

namespace Member
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