using EventCheckinSystem.Repo.Bases.BaseEntitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.Data
{
    public class Guest : BaseEntity
    {
        public int GuestID { get; set; }
        public int GuestGroupID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime Birthday { get; set; }
        public GuestGroup GuestGroup { get; set; }
        public GuestImage GuestImage { get; set; }
        public GuestCheckin GuestCheckin { get; set; }

    }


}
