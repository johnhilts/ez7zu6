using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using NSpec;
using FluentAssertions;

namespace ez7zu6.Integration.Experience
{
    public class ExperienceSpec : nspec
    {
        private string _domain = null;
        private string Domain => _domain ?? (_domain = "http://localhost:17726");

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
                    // make a GET request and get the list of experiences
                    // verify that the added experience is in the list
                };
            };
        }

        private async Task<AddExperienceResponse> AddExperience()
        {
            var experience = new TestExperience { Notes = "test 123", };
            var json = JsonConvert.SerializeObject(experience);
            var client = new HttpClient();
            var url = new Uri($"{Domain}/api/experience");
            var response = await client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var experienceId = JsonConvert.DeserializeObject<int>(jsonResponse);
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

    }

    public class TestExperience
    {
        public string Notes { get; set; }
    }

    public class AddExperienceResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public int ExperienceId { get; set; }
        public bool IsAnonymous { get; set; }
        public string Location { get; set; }
        public Guid UserSessionId { get; set; }
    }

}

