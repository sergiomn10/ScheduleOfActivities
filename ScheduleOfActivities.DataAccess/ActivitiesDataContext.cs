using Microsoft.EntityFrameworkCore;
using ScheduleOfActivities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleOfActivities.DataAccess
{
    public class ActivitiesDataContext : DbContext
    {
        public ActivitiesDataContext(DbContextOptions<ActivitiesDataContext> options) :
            base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActivityModel>()
            .HasOne(p => p.propertyModel)
            .WithMany(c => c.activitiesList)
            .HasForeignKey(p => p.property_id);

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<PropertyModel> Property { get; set; }
        public DbSet<ActivityModel> Activity { get; set; }
        public DbSet<SurveyModel> Survey { get; set; }
    }
}
