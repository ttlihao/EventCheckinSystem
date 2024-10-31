using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.DTOs.ResponseDTO
{
    public class GuestGroupResponse
    {
        public int GuestGroupID { get; set; }
        [JsonIgnore]
        public int OrganizationID { get; set; }
        public string OrganizationName { get; set; }
        public string Type { get; set; }
        [JsonIgnore]
        public int EventID { get; set; }
        public string EventName { get; set; }
        public string Name { get; set; }
    }
}
