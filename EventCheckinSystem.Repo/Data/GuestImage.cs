using EventCheckinSystem.Repo.Bases.BaseEntitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.Data
{
    public class GuestImage : BaseEntity
    {
        public int GuestImageID { get; set; }
        public int GuestID { get; set; }
        public string ImageURL { get; set; }

        public Guest Guest { get; set; }
    }

}
