﻿using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Specialized;
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
                      var expected = new AddExperienceResponse { 
                          StatusCode = HttpStatusCode.Created, IsAnonymous = true, Location = $"{Domain}/api/experience",
                          Cookies = new StringDictionary() { { "IsAnonymous", "True" }, } };

                      var actual = await AddExperience();
                      // var expectedCookies = new StringDictionary() { { "UserSession", Guid.NewGuid().ToString() }, { "IsAnonymous", "True" }, };
                      Guid.TryParse(actual.RawCookies.Single(x => x.Contains("UserSession")).Split('=')[1].Split(';')[0], out Guid userSessionId).Should().Be(true);
                      bool.TryParse(actual.RawCookies.Single(x => x.Contains("IsAnonymous")).Split('=')[1].Split(';')[0], out bool isAnonymous).Should().Be(true);
                      actual.Cookies = new StringDictionary() { { "IsAnonymous", isAnonymous.ToString() }, };
                      actual.IsAnonymous = isAnonymous;

                      actual.ShouldBeEquivalentTo(expected, options => options.Excluding(o => o.ExperienceId).Excluding(o => o.RawCookies));

                      // should be able to go to the list and located experience ID at the top of new experiences
                      // remember to re-use http client!! and I guess make it static?? (only if necessary)
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
            var actual = new AddExperienceResponse { StatusCode = response.StatusCode, Location = response.Headers.GetValues("Location").FirstOrDefault(),
                RawCookies = response.Headers.GetValues("Set-Cookie"), ExperienceId = experienceId
            };
            return actual;
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
        public IEnumerable<string> RawCookies { get; set; }
        public StringDictionary Cookies { get; set; }
    }

}

