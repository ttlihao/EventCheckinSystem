using EventCheckinSystem.Repo.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.DTOs
{
    public class UserEventDTO
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public int EventID { get; set; }   
        public string EventName { get; set; } 
        public DateTime EventDate { get; set; }
    }
}