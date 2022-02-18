using Microsoft.EntityFrameworkCore;
using ScheduleOfActivities.DataAccess;
using ScheduleOfActivities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleOfActivities.Business
{
    public class ActivityProxy
    {
        private PropertyRepository PropertyRepository;
        private ActivityRepository ActivityRepository;
        public ActivityProxy(PropertyRepository propertyRepository,
            ActivityRepository activityRepository)
        {
            PropertyRepository = propertyRepository;
            ActivityRepository = activityRepository;
        }
        public async Task<List<ActivityResponseModel>> GetActivitiesList(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                var t = await PropertyRepository.GetAllProperty();
                var result = await ActivityRepository.GetActivityListByDates(startDate, endDate);
                return result.Select(x => new ActivityResponseModel()
                {
                    id = x.id,
                    schedule = x.schedule,
                    title = x.title,
                    created_at = x.created_at,
                    status = x.status,
                    condition = (x.status.Equals("ACTIVO") && x.schedule.Date >= DateTime.Now.Date) ? "PENDIENTE A REALIZAR" :
                    ((x.status.Equals("ACTIVO") && x.schedule.Date <= DateTime.Now.Date) ? "ATRASADA" : "FINALIZADA"),
                    property = new PropertyResponseModel()
                    {
                        id = x.propertyModel.id,
                        title = x.propertyModel.title,
                        address = x.propertyModel.address
                    },
                    survey = "http://localhost:5279/survey/index/" + x.id
                }).ToList();
            }
            catch (Exception ex)
            {

                return new List<ActivityResponseModel>();
            }
        }

        public async Task<bool> CreateActivity(ActivityModel model)
        {
            try
            {
                var propertyResult = PropertyRepository.GetPropertyDetail(model.property_id).Result;
                var activitiesResult = ActivityRepository.GetActivitiesByPropertyId(model.property_id).Result
                    .Where(w => w.schedule.Date == model.schedule.Date).ToList();
                if (propertyResult.status.Equals("DESACTIVADA"))
                {
                    return false;
                }
                if (activitiesResult.Count() > 0)
                {
                    if (activitiesResult.Where(w => w.schedule.ToString("hh").Equals(model.schedule.ToString("hh"))).Count() > 0)
                    {
                        return false;
                    }
                }

                return await ActivityRepository.CreateActivity(model);
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> RescheduleActivity(ActivityModel model)
        {
            try
            {
                var activityResult = ActivityRepository.GetActivityDetail(model.id).Result;

                if (activityResult == null)
                {
                    return false;
                }

                if (activityResult.status.Equals("CANCELADA"))
                {
                    return false;
                }

                await ActivityRepository.UpdateActivity(model);

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> CancelActivity(int id)
        {
            try
            {
                var activityResult = ActivityRepository.GetActivityDetail(id).Result;

                if (activityResult == null)
                {
                    return false;
                }

                activityResult.status = "CANCELADA";

                await ActivityRepository.UpdateActivity(activityResult);

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
