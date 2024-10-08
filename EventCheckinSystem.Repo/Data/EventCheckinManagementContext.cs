using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        public DbSet<User> Users { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<GuestGroup> GuestGroups { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<WelcomeTemplate> WelcomeTemplates { get; set; }
        public DbSet<GuestImage> GuestImages { get; set; }
        public DbSet<GuestCheckin> GuestCheckins { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Organization has many Events
            builder.Entity<Organization>()
                .HasMany(o => o.Events)
                .WithOne(e => e.Organization)
                .HasForeignKey(e => e.OrganizationID)
                .OnDelete(DeleteBehavior.Restrict);

            // Event has many GuestGroups
            builder.Entity<Event>()
                .HasMany(e => e.GuestGroups)
                .WithOne(gg => gg.Event)
                .HasForeignKey(gg => gg.EventID)
                .OnDelete(DeleteBehavior.Cascade);

            // GuestGroup has many Guests
            builder.Entity<GuestGroup>()
                .HasMany(gg => gg.Guests)
                .WithOne(g => g.GuestGroup)
                .HasForeignKey(g => g.GuestGroupID)
                .OnDelete(DeleteBehavior.Cascade);

            // GuestGroup has one WelcomeTemplate
            builder.Entity<GuestGroup>()
                .HasOne(gg => gg.WelcomeTemplate)
                .WithOne(wt => wt.GuestGroup)
                .HasForeignKey<WelcomeTemplate>(wt => wt.GuestGroupID)
                .OnDelete(DeleteBehavior.Cascade);

            // Guest has one GuestImage
            builder.Entity<Guest>()
                .HasOne(g => g.GuestImage)
                .WithOne(gi => gi.Guest)
                .HasForeignKey<GuestImage>(gi => gi.GuestID)
                .OnDelete(DeleteBehavior.Cascade);

            // Guest has one GuestCheckin
            builder.Entity<Guest>()
                .HasOne(g => g.GuestCheckin)
                .WithOne(gc => gc.Guest)
                .HasForeignKey<GuestCheckin>(gc => gc.GuestID)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure many-to-many relationship between User and Event
            builder.Entity<UserEvent>()
                .HasKey(ue => new { ue.UserID, ue.EventID });

            builder.Entity<UserEvent>()
                .HasOne(ue => ue.User)
                .WithMany(u => u.UserEvents)
                .HasForeignKey(ue => ue.UserID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserEvent>()
                .HasOne(ue => ue.Event)
                .WithMany(e => e.UserEvents)
                .HasForeignKey(ue => ue.EventID)
                .OnDelete(DeleteBehavior.Restrict);
        }


    }
}
