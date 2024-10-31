using EventCheckinSystem.Repo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.DTOs.ResponseDTO
{
    public class EventResponse
    {
        public int EventID { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public int OrganizationID { get; set; }
        public string OrganizationName{ get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public string Status { get; set; }
    }


    
}

