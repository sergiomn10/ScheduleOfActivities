using Microsoft.EntityFrameworkCore;
using ScheduleOfActivities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleOfActivities.DataAccess
{
    public class PropertyRepository
    {
        private ActivitiesDataContext DB;
        public PropertyRepository(ActivitiesDataContext db)
        {
            DB = db;
        }
        

        public async Task<List<PropertyModel>> GetAllProperty()
        {
            return await DB.Property.ToListAsync();
        }

        public async Task<PropertyModel?> GetPropertyDetail(int id)
        {
            return await DB.Property.FirstOrDefaultAsync(f => f.id == id);
        }

    }
}
