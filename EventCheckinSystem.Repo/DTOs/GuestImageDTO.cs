using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.DTOs
{
    public class GuestImageDTO
    {
        public int GuestImageID { get; set; }
        public int GuestID { get; set; }
        public string ImageURL { get; set; }
    }
}
