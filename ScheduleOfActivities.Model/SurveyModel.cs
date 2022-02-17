using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleOfActivities.Model
{
    public class SurveyModel
    {
        public int Id { get; set; }
        public int activity_id { get; set; }
        public string answers { get; set; } = string.Empty;
        public DateTime created_at { get; set; }

    }
}
