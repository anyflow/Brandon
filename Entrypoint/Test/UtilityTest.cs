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
    public class UtilityTest
    {
        private string GenerateJson()
        {
            List<User> users = new List<User>();
            var user1 = new User()
            {
                Id = "id1",
                Name = "name1",
                Description = "description1",
                CreateDate = DateTime.Now
            };
            users.Add(user1);

            var user2 = new User()
            {
                Id = "id2",
                Name = "name2",
                Description = "description2",
                CreateDate = DateTime.Now
            };
            users.Add(user2);

            return JsonConvert.SerializeObject(users);
        }

        [TestMethod]
        public void TestSerialization()
        {
            string json = GenerateJson();

            System.Diagnostics.Debug.Write(json);

            StringAssert.Contains(json, "name2");
        }

        [TestMethod]
        public void TestDeserialization()
        {
            string json = GenerateJson();

            List<User> users = JsonConvert.DeserializeObject<List<User>>(json);

            Assert.AreEqual<String>("name2", users[1].Name);
        }
    }
}