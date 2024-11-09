using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.DTOs.ResponseDTO
{
    public class GuestResponse
    {
        public int GuestID { get; set; }
        public int GuestGroupID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime Birthday { get; set; }
        public string? ImageURL { get; set; }
        public bool checkinStatus { get; set; } = false;

    }
}
