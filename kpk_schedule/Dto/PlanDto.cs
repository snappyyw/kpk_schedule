using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kpk_schedule.Dto
{
    internal class PlanDto
    {
        public int Id { get; set; }
        public int? WorkTime { get; set; }
        public string LessonName { get; set; }
        public string TeachersName { get; set; }
    }
}
