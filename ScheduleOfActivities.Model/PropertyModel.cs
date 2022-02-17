using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleOfActivities.Model
{
    public  class PropertyModel
    {
        [Key]
        public int id { get; set; }
        public string title { get; set; } = string.Empty;
        public string address { get; set; } = string.Empty;

        public string description { get; set; } = string.Empty;

        public DateTime created_at { get; set; }

        public DateTime updated_at { get; set; }

        public DateTime? disabled_at { get; set; }

        public string status { get; set; } = string.Empty;

        public List<ActivityModel>  activitiesList { get; set; }
    }
}
