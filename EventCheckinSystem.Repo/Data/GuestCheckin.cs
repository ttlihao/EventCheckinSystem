using EventCheckinSystem.Repo.Bases.BaseEntitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.Data
{
    public class GuestCheckin : BaseEntity
    {
        public int GuestCheckinID { get; set; }
        public int GuestID { get; set; }
        public DateTime CheckinTime { get; set; }

        public Guest Guest { get; set; }
    }


}
