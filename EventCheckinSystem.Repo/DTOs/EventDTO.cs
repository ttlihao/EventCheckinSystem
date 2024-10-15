using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.DTOs
{
    public class EventDTO
    {
        public int EventID { get; set; }
        public string Name { get; set; }
        public int OrganizationID { get; set; }
        public string OrganizationName { get; set; }
        public List<GuestGroupDTO> GuestGroups { get; set; }
        public List<UserEventDTO> UserEvents { get; set; }
    }

    
}

