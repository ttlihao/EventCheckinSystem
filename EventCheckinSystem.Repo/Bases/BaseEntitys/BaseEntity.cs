using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.Bases.BaseEntitys
{
    public class BaseEntity : IBaseEntity
    {
        public DateTimeOffset CreatedTime { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset LastUpdatedTime { get; set; }
        public DateTimeOffset? DeletedTime { get; set; }

        public required string CreatedBy { get; set; }
        public required string LastUpdatedBy { get; set; }
        public string? DeletedBy { get; set; }

        public bool IsActive { get; set; } = true;
        public bool IsDelete { get; set; } = false;
    }
}
