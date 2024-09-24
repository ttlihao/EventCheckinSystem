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
    }

}
