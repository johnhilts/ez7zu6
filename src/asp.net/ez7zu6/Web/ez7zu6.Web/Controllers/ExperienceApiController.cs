using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ez7zu6.Core.Models.Web;
using ez7zu6.Member.Models;
using ez7zu6.Member.Services;
using ez7zu6.Web.Models.Experience;
using ez7zu6.Web.Services;

namespace ez7zu6.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/experience")]
    public class ExperienceApiController : BaseController
    {
        public ExperienceApiController(IApplicationService applicationService) 
            : base(applicationService) { }

        [HttpGet]
        public async Task<IActionResult> Get(int? previousIndex)
        {
            var userSession = PresentationService.GetOrCreateUserSession();
            var experiences = await (new MemberService(_applicationService.ApplicationSettings).GetExperiences(userSession.UserId, previousIndex));
            return Ok(experiences);
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
            //LogSomeInfo();

            var userSession = PresentationService.GetOrCreateUserSession();
            model.InputDateTime = DateTime.Now;
            var experienceId = await (new MemberService(_applicationService.ApplicationSettings).SaveExperience(model, userSession.UserId));
            var createdModel = 
                new ExperienceCreatedModel
                {
                    Value = experienceId,
                    Links = new List<HateoasLinkModel>
                    {
                        new HateoasLinkModel { Label = "list", Link = new Uri($"{_applicationService.SiteSettings.Domain}/api/experience") }
                    },
                };
            return Created(new Uri($"{_applicationService.SiteSettings.Domain}/api/experience"), createdModel);
        }

        private void LogSomeInfo()
        {
            bool.TryParse(System.Environment.GetEnvironmentVariable("IS_APPHARBOR"), out bool isAppHarbor);
            var msg = $"IS_APPHARBOR = {isAppHarbor}, Domain = {_applicationService.SiteSettings.Domain}";
            throw new ApplicationException(msg);
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
