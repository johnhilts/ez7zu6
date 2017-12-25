using System.Collections.Generic;

namespace ez7zu6.Core.Models.Web
{
    public class HateoasResponseModel<T>
    {
        public virtual T Value { get; set; }
        public List<HateoasLinkModel> Links { get; set; }
    }
}
