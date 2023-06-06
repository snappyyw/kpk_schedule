using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kpk_schedule.Dto
{
    internal class ScheduleDto
    {
        public int Id { get; set; }
        public int? WorkTime { get; set; }
        public string LessonName { get; set; }
        public int LessonId { get; set; }
        public string TeachersName { get; set; }
        public int TeachersId { get; set; }
        public string GroupName { get; set; }
        public int GroupId { get; set; }
        public string CouplesName { get; set; }
        public string CabinetName { get; set; }
        public DateTime ScheduleDate { get; set; } 
    }
}
