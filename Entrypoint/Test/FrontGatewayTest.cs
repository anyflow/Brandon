using Brandon.Gateway;
using Brandon.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brandon.Test
{
    [TestClass]
    public class FrontGatewayTest
    {
        private string baseAddress = "http://192.168.0.5:8090/room";
        private string inviterId = "09bab373-ff80-42ca-a221-ba3e8345a469";
        private List<string> inviteeIds = new List<string>() { "9658b662-9352-46d5-b778-908bc90204a6", "7db4ee17-5e7c-4a8e-b868-571168f7fcb1" };

        [TestMethod]
        public void createRoomTest()
        {
            MyInformation.Me = new User()
            {
                Id = "09bab373-ff80-42ca-a221-ba3e8345a469",
                Name = "anyflow",
                Description = "anyflow.net",
                CreateDate = DateTime.Now
            };

            var fg = new FrontGateway();
            var ret = fg.CreateRoom("sampleName", "sampleMessage", inviteeIds).Result;

            System.Diagnostics.Debug.Write(ret.Item1.Id);

            Assert.IsInstanceOfType(ret.Item1, typeof(Room));
        }

        [TestMethod]
        public void testGetHttpbin()
        {
            var fg = new FrontGateway();
            var ret = fg.getHttpbin().Result;

            System.Diagnostics.Debug.WriteLine(ret.Content.ToString());
            Assert.IsNotNull(ret.Content);
        }
    }
}
