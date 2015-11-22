using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Brandon.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Brandon.Gateway
{
    public class FrontGateway
    {
        private String ServerBaseAddress { get; set; }
        
        public FrontGateway()
        {
            ServerBaseAddress = "http://localhost:8080/";
        }

        public static Int32 toUnixTimestamp(DateTime input)
        {
            return (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }

        public static DateTime toDateTime(Int32 unixTimestamp)
        {
            DateTime sTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return sTime.AddSeconds(unixTimestamp);
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
                
                var request = new HttpRequestMessage(HttpMethod.Post, ServerBaseAddress + "room");
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.SendAsync(request);
                
                if(response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new HttpRequestException();
                    //TODO logging...
                }

                var responseContent = await response.Content.ReadAsStringAsync();

                var resJson = new JObject(responseContent);

                var room = new Room()
                {
                    Id = resJson["roomId"].ToString(),
                    CreateDate = toDateTime(Int32.Parse(resJson["createDate"].ToString()))
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