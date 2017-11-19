using System;
using NSpec;
using FluentAssertions;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace Integration.Experience
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
                      var url = new Uri("http://localhost:17726/api/experience");
                      var response = await client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
                      response.IsSuccessStatusCode.Should().Be(true);
                      var expected = "OK";
                      var actual = await response.Content.ReadAsStringAsync();
                      actual.Should().Be(expected);
                  };
            };
        }
    }

    public class TestExperience
    {
        public string Notes { get; set; }
    }
}

