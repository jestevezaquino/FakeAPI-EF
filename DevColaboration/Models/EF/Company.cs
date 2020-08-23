using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevColaboration.Models.EF
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CatchPhrase { get; set; }
        public string Bs { get; set; }
        public List<User> Users { get; set; }
    }
}
