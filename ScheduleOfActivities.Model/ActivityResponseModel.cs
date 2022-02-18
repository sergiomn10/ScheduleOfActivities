using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleOfActivities.Model
{
    public  class ActivityResponseModel
    {
        public int id { get; set; }
        public DateTime schedule { get; set; }
        public string title { get; set; }
        public DateTime created_at { get; set; }
        public string status { get; set; }
        
        public string condition { get; set; }

        public PropertyResponseModel property { get; set; }

        public string survey { get; set; }
    }
}
