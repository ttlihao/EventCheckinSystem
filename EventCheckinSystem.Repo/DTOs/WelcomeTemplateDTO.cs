using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.DTOs
{
    public class WelcomeTemplateDTO
    {
        public int WelcomeTemplateID { get; set; } 
        public int GuestGroupID { get; set; }
        public string TemplateContent { get; set; } 
        public string GuestGroupName { get; set; } 
    }
}