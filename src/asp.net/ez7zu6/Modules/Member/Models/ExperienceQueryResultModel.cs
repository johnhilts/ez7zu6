using System.Collections.Generic;

namespace ez7zu6.Member.Models
{
    public class ExperienceQueryResultModel
    {
        public List<ExperienceQueryModel> Experiences { get; set; }
        public int EndIndex { get; set; }
        public int TotalRowCount { get; set; }
    }
}