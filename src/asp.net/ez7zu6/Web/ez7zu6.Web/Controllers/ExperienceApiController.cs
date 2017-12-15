using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ez7zu6.Core;
using ez7zu6.Member.Models;
using ez7zu6.Member.Services;

namespace ez7zu6.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/experience")]
    public class ExperienceApiController : BaseController
    {
        public ExperienceApiController(IOptions<SiteSettings> siteSettings, IAppEnvironment appEnvironment) : base(siteSettings, appEnvironment)
        {
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ExperienceSaveModel model)
        {
            //var notes = Notes.GetValue("Notes").ToString();
            //ExperienceSaveModel model = new ExperienceSaveModel
            //{
            //    UserId = new Guid(),
            //    Notes = notes,
            //    InputDateTime = DateTime.Now,
            //    Created = DateTime.Now,
            //    IsActive = true,
            //};
            // TODO: this whole approach looks very unDRY ... need to cleanup!
            if (PresentationService.IsAnonymousSession())
            {
                var userSession = PresentationService.GetOrCreateUserSession(null);
                model.UserId = userSession.UserId;
            }
            else
            {
                // not sure about this 
                var userSession = PresentationService.GetOrCreateUserSession(model.UserId);
            }
            model.InputDateTime = DateTime.Now;
            var experienceId = await (new MemberService(_appEnvironment).SaveExperience(model));
            return Created(new Uri($"{_siteSettings.Value.Domain}/api/experience"), experienceId);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
