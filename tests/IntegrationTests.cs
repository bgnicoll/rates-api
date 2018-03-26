using NUnit.Framework;
using System.Net.Http;
using System.Net.Http.Headers;
using System;

namespace Tests
{
    [TestFixture]
    public class IntegrationTests
    {
        [Test]
        public void POSTNewRates_RetrievePrice()
        {
            var httpClient = new HttpClient
            {
                Timeout = new TimeSpan(0,0,20),
                BaseAddress = new Uri("http://localhost:8080/")
            };
            var buffer = System.Text.Encoding.UTF8.GetBytes(GetSamplePOSTData());
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var responsePost = httpClient.PostAsync("/api/rates", byteContent).Result;
            Assert.AreEqual(true, responsePost.IsSuccessStatusCode);
            Assert.AreEqual(responsePost.Content.ReadAsStringAsync().Result, "{\"newRates\":12}");

             var responseGet = httpClient.GetAsync("api/rates?start=2015-07-01T07%3A00%3A00Z&end=2015-07-01T12%3A00%3A00Z").Result;
             Assert.AreEqual(true, responseGet.IsSuccessStatusCode);
             Assert.AreEqual(responseGet.Content.ReadAsStringAsync().Result, "{\"price\":\"1,750\"}");
        }

        private string GetSamplePOSTData()
        {
            return "{ \"rates\": [ { \"days\": \"mon,tues,thurs\", \"times\": \"0900-2100\", \"price\": 1500 }, { \"days\": \"fri,sat,sun\", \"times\": \"0900-2100\", \"price\": 2000 }, { \"days\": \"wed\", \"times\": \"0600-1800\", \"price\": 1750 }, { \"days\": \"mon,wed,sat\", \"times\": \"0100-0500\", \"price\": 1000 }, { \"days\": \"sun,tues\", \"times\": \"0100-0700\", \"price\": 925 } ] }";
        }
    }
}
