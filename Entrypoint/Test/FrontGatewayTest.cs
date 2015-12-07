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
        private List<string> inviteeIds = new List<string>() { "9658b662-9352-46d5-b778-908bc90204a6", "7db4ee17-5e7c-4a8e-b868-571168f7fcb1" };

        [TestMethod]
        public void TestCreateRoom()
        {
            MyInformation.Me = new User()
            {
                Id = "09bab373-ff80-42ca-a221-ba3e8345a469",
                Name = "anyflow",
                Description = "anyflow.net",
                CreateDate = DateTime.Now
            };

            var ret = FrontGateway.Self.CreateRoom("sampleName", "sampleMessage", inviteeIds).Result;

            System.Diagnostics.Debug.Write(ret.Item1.Id);

            Assert.IsInstanceOfType(ret.Item1, typeof(Room));
        }

        [TestMethod]
        public void TestGetHttpbin()
        {
            var ret = FrontGateway.Self.getHttpbin().Result;

            System.Diagnostics.Debug.WriteLine(ret.Content.ToString());
            Assert.IsNotNull(ret.Content);
        }
    }
}