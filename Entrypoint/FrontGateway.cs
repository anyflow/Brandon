using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Brandon.Model;
using Newtonsoft.Json;

namespace Brandon.Gateway
{
    public class FrontGateway
    {
        private String ServerAddress { get; set; }
        
        public async void CreateSession()
        {
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Post, ServerAddress);
                request.Content = new StringContent("", Encoding.UTF8, "application/json");

                var response = await client.SendAsync(request);

                var responseContent = await response.Content.ReadAsStringAsync();
            }
        }
    }
}