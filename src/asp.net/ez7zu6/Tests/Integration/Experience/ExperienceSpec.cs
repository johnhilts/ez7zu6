using System;
using System.Net.Http;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using NSpec;
using FluentAssertions;

namespace ez7zu6.Integration.Experience
{
    public class ExperienceSpec : nspec
    {
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

