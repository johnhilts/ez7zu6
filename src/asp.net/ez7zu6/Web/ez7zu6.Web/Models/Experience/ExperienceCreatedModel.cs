using System;
using ez7zu6.Core.Models.Web;

namespace ez7zu6.Web.Models.Experience
{
    public class ExperienceCreatedModel : HateoasResponseModel<Guid>
    {
        public override Guid Value { get; set; }
    }
}
