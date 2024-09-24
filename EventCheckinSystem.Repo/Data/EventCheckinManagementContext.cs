using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.Data
{
    public class EventCheckinManagementContext : IdentityDbContext
    {
        public EventCheckinManagementContext(DbContextOptions<EventCheckinManagementContext> options) : base(options)
        {
        }
    }
}
