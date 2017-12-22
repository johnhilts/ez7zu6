using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NSpec;
using FluentAssertions;
using System.Collections.Generic;
using ez7zu6.Member.Models;

namespace ez7zu6.Integration.Experience
{
    public class ExperienceSpec : nspec
    {
        private string _domain = null;
        private string Domain => _domain ?? (_domain = "http://localhost:17726");
        private HttpClient _client;

        void experience_rest_api()
        {
            context["adding"] = () =>
            {
                itAsync["can add an experience"] = async () =>
                  {
                      var expected = new AddExperienceResponse { StatusCode = HttpStatusCode.Created, IsAnonymous = true, Location = $"{Domain}/api/experience", };
                      var actual = await AddExperience();
                      actual.ShouldBeEquivalentTo(expected, options => options.Excluding(o => o.ExperienceId).Excluding(o => o.UserSessionId));
                      // should be able to go to the list and located experience ID at the top of new experiences
                      // remember to re-use http client!! and I guess make it static?? (only if necessary)
                  };

                itAsync["can see an experience in a list after adding it"] = async () =>
                {
                    var experience = await AddExperience();
                    var experienceList = await GetExperienceList();
                    experienceList.Single(x => x.ExperienceId == experience.ExperienceId).Should().NotBeNull();
                };
            };
        }

        private async Task<AddExperienceResponse> AddExperience()
        {
            var experience = new TestExperience { Notes = "test 123", };
            var json = JsonConvert.SerializeObject(experience);
            var url = new Uri($"{Domain}/api/experience");
            _client = new HttpClient(); // want to use a new client to force setting of cookies
            var response = await _client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var experienceId = JsonConvert.DeserializeObject<Guid>(jsonResponse);
            var cookies = response.Headers.GetValues("Set-Cookie");
            Guid.TryParse(cookies.Single(x => x.Contains("UserSession")).Split('=')[1].Split(';')[0], out Guid userSessionId);
            bool.TryParse(cookies.Single(x => x.Contains("IsAnonymous")).Split('=')[1].Split(';')[0], out bool isAnonymous);

            return new AddExperienceResponse
            {
                ExperienceId = experienceId,
                StatusCode = response.StatusCode,
                Location = response.Headers.GetValues("Location").FirstOrDefault(),
                IsAnonymous = isAnonymous,
                UserSessionId = userSessionId,
            };
        }

        private async Task<List<ExperienceQueryModel>> GetExperienceList()
        {
            var url = new Uri($"{Domain}/api/experience");
            var response = await _client.GetAsync(url);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ExperienceQueryModel>>(jsonResponse);
        }

    }

    public class TestExperience
    {
        public string Notes { get; set; }
    }

    public class AddExperienceResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public Guid ExperienceId { get; set; }
        public bool IsAnonymous { get; set; }
        public string Location { get; set; }
        public Guid UserSessionId { get; set; }
    }

}

