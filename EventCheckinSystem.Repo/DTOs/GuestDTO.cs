using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.DTOs
{
    public class GuestDTO
    {
        public int GuestID { get; set; }
        public int GuestGroupID { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
    }
}
