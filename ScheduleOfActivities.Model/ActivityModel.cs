using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleOfActivities.Model
{
    public class ActivityModel
    {
        [Key]
        public int id { get; set; }
        
        public int property_id { get; set; }
        public DateTime schedule { get; set; }
        public string title { get; set; } = string.Empty;
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string status { get; set; } = string.Empty;

        [ForeignKey("id")]
        public virtual PropertyModel propertyModel { get; set; }
        public List<SurveyModel> surveysList { get; set; }
    }
}
