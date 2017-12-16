using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Caching.Memory;
using ez7zu6.Core;
using ez7zu6.Member.Models;
using ez7zu6.Member.Services;

namespace ez7zu6.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/experience")]
    public class ExperienceApiController : BaseController
    {
        public ExperienceApiController(IOptions<SiteSettings> siteSettings, IAppEnvironment appEnvironment, IMemoryCache memoryCache) : base(siteSettings, appEnvironment, memoryCache)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userSession = PresentationService.GetOrCreateUserSession();
            var experiences = await (new MemberService(_appEnvironment).GetExperiences(userSession.UserId));
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
            var userSession = PresentationService.GetOrCreateUserSession();
            model.InputDateTime = DateTime.Now;
            var experienceId = await (new MemberService(_appEnvironment).SaveExperience(model, userSession.UserId));
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
