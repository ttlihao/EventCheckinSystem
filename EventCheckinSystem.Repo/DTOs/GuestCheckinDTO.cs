using EventCheckinSystem.Repo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.DTOs
{
    public class GuestCheckinDTO
    {
        public int GuestCheckinID { get; set; }
        public int GuestID { get; set; }
        public DateTime CheckinTime { get; set; }
        public DateTime? CheckoutTime { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }

    }
}
