using ScheduleOfActivities.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleOfActivities.DataAccess
{
    public class ActivityRepository
    {
        private ActivitiesDataContext DB;
        public ActivityRepository(ActivitiesDataContext db)
        {
            DB = db;
        }

        public async Task<bool> CreateActivity(ActivityModel model)
        {
            await DB.Activity.AddAsync(model);
            await DB.SaveChangesAsync();

            return true;
        }

        public async Task<List<ActivityModel>> GetActivitiesByPropertyId(int property_id)
        {
           return await DB.Activity.Where(w => w.property_id == property_id).ToListAsync();            
        }

        public async Task<ActivityModel?> GetActivityDetail(int id)
        {
            return await DB.Activity.FirstOrDefaultAsync(w => w.id == id);

        }

        public async Task<bool> UpdateActivity(ActivityModel model) 
        {
            var result = await DB.Activity.SingleOrDefaultAsync(s => s.id == model.id);
            if (result != null)
            {
                result.schedule = model.schedule;
                result.status = model.status;
                await DB.SaveChangesAsync();
            }
            return true;
        }
    }
}
