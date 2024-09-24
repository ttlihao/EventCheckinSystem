using EventCheckinSystem.Repo.Bases.BaseEntitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.Data
{
    public class WelcomeTemplate : BaseEntity
    {
        public int WelcomeTemplateID { get; set; }
        public int GuestGroupID { get; set; }
        public string TemplateContent { get; set; }

        public GuestGroup GuestGroup { get; set; }
    }

}
