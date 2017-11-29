using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ez7zu6.Core;
using ez7zu6.Member.Models;
using ez7zu6.Member.Services;

namespace ez7zu6.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/experience")]
    public class ExperienceApiController : BaseController
    {
        private readonly IAppEnvironment _appEnvironment;

        public ExperienceApiController(IAppEnvironment appEnvironment) => _appEnvironment = appEnvironment;

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
            if (PresentationService.IsAnonymousSession())
            {
                var userSession = PresentationService.GetOrCreateUserSession(null);
                model.UserId = userSession.UserId;
            }
            model.InputDateTime = DateTime.Now;
            await (new MemberService(_appEnvironment).SaveExperience(model));
            // TODO: I want to return 201, and the inserted ID
            //return Created(new Uri("http://localhost:17726/api/experience"), new CreatedResult(new Uri("http://localhost:17726/api/experience"), "OK"));
            return Created(new Uri("http://localhost:17726/api/experience"), "OK");
            //return "OK";
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
