using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Net;
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
                      var experience = new TestExperience { Notes = "test 123", };
                      var json = JsonConvert.SerializeObject(experience);
                      var client = new HttpClient();
                      var url = new Uri("http://localhost.:17726/api/experience");
                      var response = await client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
                      var expected = new ExpectedResponse { Value = "OK", /* change this to added ID; we need to check if it's a GUID*/
                          StatusCode = HttpStatusCode.Created, IsAnonymous = true, Location = $"{Domain}/api/experience",
                          Cookies = new StringDictionary() { { "IsAnonymous", "True" }, } };
                      var jsonResponse = await response.Content.ReadAsStringAsync();
                      var actual = new ExpectedResponse { Value = JsonConvert.DeserializeObject<string>(jsonResponse) };
                      actual.StatusCode = response.StatusCode;

                      // var expectedCookies = new StringDictionary() { { "UserSession", Guid.NewGuid().ToString() }, { "IsAnonymous", "True" }, };
                      var actualCookies = response.Headers.GetValues("Set-Cookie");
                      Guid.TryParse(actualCookies.Single(x => x.Contains("UserSession")).Split('=')[1].Split(';')[0], out Guid userSessionId).Should().Be(true);
                      bool.TryParse(actualCookies.Single(x => x.Contains("IsAnonymous")).Split('=')[1].Split(';')[0], out bool isAnonymous).Should().Be(true);
                      actual.Cookies = new StringDictionary() { { "IsAnonymous", isAnonymous.ToString() }, };
                      actual.IsAnonymous = isAnonymous;

                      actual.Location = response.Headers.GetValues("Location").FirstOrDefault();

                      actual.ShouldBeEquivalentTo(expected);
                  };
            };
        }
    }

    public class TestExperience
    {
        public string Notes { get; set; }
    }

    public class ExpectedResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Value { get; set; }
        public bool IsAnonymous { get; set; }
        public string Location { get; set; }
        public StringDictionary Cookies { get; set; }
    }

}

