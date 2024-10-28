using EventCheckinSystem.Repo.Bases.BaseEntitys;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.Data
{
    public class Organization : BaseEntity
    {
        public int OrganizationID { get; set; }
        public string Name { get; set; }
        public ICollection<Event> Events { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public DateTime EstablishedDate { get; set; }
    }


}
