﻿using EventCheckinSystem.Repo.Bases.BaseEntitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.Data
{
    public class Event : BaseEntity
    {
        public int EventID { get; set; }
        public string Name { get; set; }
        public int OrganizationID { get; set; }
        public Organization Organization { get; set; }
        public ICollection<GuestGroup> GuestGroups { get; set; }
        public ICollection<UserEvent> UserEvents { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public string Status { get; set; }
    }


}
