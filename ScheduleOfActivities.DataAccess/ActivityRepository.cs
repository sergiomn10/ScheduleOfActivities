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

        public async Task<List<ActivityModel>> GetActivityListByDates(DateTime? startDate, DateTime? endDate)
        {
            var r = await DB.Activity.ToListAsync();
            if (startDate == null || endDate == null)
            {
               
                return await DB.Activity.Where(w => DateTime.Now.AddDays(-3) <=  w.schedule.Date 
                                                && w.schedule.Date <= DateTime.Now.AddDays(2).Date).ToListAsync();
            }

            DateTime sd = startDate.HasValue ? startDate.Value : DateTime.Now;
            DateTime ed = endDate.HasValue ? endDate.Value : DateTime.Now;
            return await DB.Activity.Where(w => sd.Date  >= w.schedule.Date
                                               && w.schedule.Date <= ed.Date).ToListAsync();

        }

        public async Task<bool> UpdateActivity(ActivityModel model) 
        {
            var result = await DB.Activity.SingleOrDefaultAsync(s => s.id == model.id);
            if (result != null)
            {
                result.schedule = model.schedule;
                result.status = model.status;
                result.updated_at = DateTime.Now;
                await DB.SaveChangesAsync();
            }
            return true;
        }
    }
}
