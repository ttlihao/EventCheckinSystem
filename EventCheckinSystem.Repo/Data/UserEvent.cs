using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.Data
{
    public class UserEvent
    {
        public string UserID { get; set; }
        public User User { get; set; }

        public int EventID { get; set; }
        public Event Event { get; set; }
    }

}
