using EventCheckinSystem.Repo.Bases.BaseEntitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.Data
{
    public class GuestGroup : BaseEntity
    {
        public int GuestGroupID { get; set; }
        public int OrganizationID { get; set; }
        public string Type { get; set; }

        public int EventID { get; set; }
        public string Name { get; set; }

        public Organization Organization { get; set; }
        public Event Event { get; set; }

        public ICollection<Guest> Guests { get; set; }
        public WelcomeTemplate WelcomeTemplate { get; set; }
    }

}
