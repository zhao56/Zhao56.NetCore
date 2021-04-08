using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;
using Zhao56.Core.BaseModel;
using Zhao56.WebApi;

namespace Zhao56.Test
{
    public class UnitTest1
    {
        public UnitTest1()
        {
            var server = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>());
            Client = server.CreateClient();
        }

        public HttpClient Client { get; }
        [Fact]
        public async Task Test1()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>
            {

                {"Page","1"},
                {"Rows","10" },
                {"Sort","1" },
                {"Order","2" },
                {"searchParameters",new List<SearchParameters>().ToString()}
            };
            HttpContent context = new FormUrlEncodedContent(dict);
            context.Headers.ContentType = new  MediaTypeHeaderValue("application/json");
            // Act
            var response = await Client.PostAsync($"/api/Builder/GetPageData", context);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            Assert.Equal("sto", result);
            Console.WriteLine($"response---{response.ToString()}");
        }
    }
}
