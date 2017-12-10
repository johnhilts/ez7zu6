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
                      var expected = "OK";
                      var jsonResponse = await response.Content.ReadAsStringAsync();
                      var actual = JsonConvert.DeserializeObject<string>(jsonResponse);
                      //var actual = JsonConvert.DeserializeObject<ExperienceAddResponse>(jsonResponse);
                      //actual.StatusCode.Should().Be((int)HttpStatusCode.Created);
                      //actual.Value.Should().Be(expected);
                      response.StatusCode.Should().Be(HttpStatusCode.Created);
                      actual.Should().Be(expected);

                      var expectedCookies = new StringDictionary() { { "UserSession", Guid.NewGuid().ToString() }, { "IsAnonymous", "True" }, };
                      var actualCookies = response.Headers.GetValues("Set-Cookie");
                      Guid.TryParse(actualCookies.Single(x => x.Contains("UserSession")).Split('=')[1].Split(';')[0], out Guid throwawayGuid).Should().Be(true);
                      bool.TryParse(actualCookies.Single(x => x.Contains("IsAnonymous")).Split('=')[1].Split(';')[0], out bool isAnonymous).Should().Be(true);
                      isAnonymous.Should().Be(true);

                      var expectedLocation = $"{Domain}/api/experience";
                      var actualLocation = response.Headers.GetValues("Location").FirstOrDefault();
                      actualLocation.Should().Be(expectedLocation);

                  };
            };
        }
    }

    public class TestExperience
    {
        public string Notes { get; set; }
    }

public class ExperienceAddResponse
    {
        public int StatusCode { get; set; }
        public string Value { get; set; }
    }
}

