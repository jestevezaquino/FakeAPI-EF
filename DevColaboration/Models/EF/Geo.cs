using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevColaboration.Models.EF
{
    public class Geo
    {
        public int Id { get; set; }
        public float Lat { get; set; }
        public float Lng { get; set; }
        public Address Address { get; set; }
    }
}
