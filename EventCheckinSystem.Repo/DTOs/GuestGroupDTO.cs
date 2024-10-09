using EventCheckinSystem.Repo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.DTOs
{
    public class GuestGroupDTO
    {
        public int GuestGroupID { get; set; }
        public int OrganizationID { get; set; }
        public int EventID { get; set; }
        public string Name { get; set; }
        public ICollection<GuestDTO> Guests { get; set; }
    }
}
