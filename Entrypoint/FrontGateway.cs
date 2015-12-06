using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Brandon.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

namespace Brandon.Gateway
{
    public class FrontGateway
    {
        private String ServerBaseAddress { get; set; }
        
        public FrontGateway()
        {
            ServerBaseAddress = "http://192.168.0.5:8090";
        }

        public static Int32 toUnixTimestamp(DateTime input)
        {
            return (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }

        public static DateTime toDateTime(Int64 unixTimestamp)
        {
            DateTime sTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return sTime.AddTicks(unixTimestamp);
        }

        public async Task<HttpResponseMessage> getHttpbin()
        {
            using (var client = new HttpClient())
            {
                return await client.GetAsync("https://httpbin.org/get?foo=bar");
            }
        }

        public async Task<Tuple<Room, MessageT>> CreateRoom(String name, String message, List<string> inviteeIds)
        {
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(new
                {
                    name = name,
                    secretKey = Encryption.GenerateSecretKey(),
                    inviterId = MyInformation.Me.Id,
                    inviteeIds = inviteeIds,
                    message = message
                });
                
                var request = new HttpRequestMessage();

                client.DefaultRequestHeaders.ExpectContinue = false;

                var response = await client.PostAsync(ServerBaseAddress + "/room", new StringContent(json, Encoding.UTF8, "application/json"));
                                
                if(response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new HttpRequestException();
                    //TODO logging...
                }

                var responseContent = await response.Content.ReadAsStringAsync();

                var resJson = JObject.Parse(responseContent);
                
                var room = new Room()
                {
                    Id = resJson["id"].ToString(),
                    CreateDate = toDateTime(Int64.Parse(resJson["createDate"].ToString()))
                };

                var messageRet = new MessageT()
                {
                    Message = message,
                    CreateDate = room.CreateDate,
                    CreatorId = MyInformation.Me.Id
                };

                return new Tuple<Room, MessageT>(room, messageRet);
            }
        }
    }
}