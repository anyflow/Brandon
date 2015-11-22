using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brandon.Model
{
    public class Room
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string SecretKey { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
