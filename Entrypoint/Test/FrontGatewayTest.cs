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
        [TestMethod]
        public void createRoomTest()
        {
            var fg = new FrontGateway();

            var ret = fg.CreateRoom("sampleName", "sampleMessage", new List<string>());
            
            //System.Diagnostics.Debug.Write(json);

            //StringAssert.Contains(json, "name2");
        }
    }
}
