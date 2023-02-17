using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace C971ScheduleApp.TermsCourses
{
    public class PerformanceAssessment
    {
        [PrimaryKey, AutoIncrement]
        public int perfAssessmentId { get; set; }
        public int courseId { get; set; }
        public string perfAssessmentName { get; set; }
        public string perfAssessemntType { get; set; }
        public bool perfAssessmentNotification { get; set; }
        public DateTime startPerfAssessment { get; set; }
        public DateTime endPerfAssessment { get; set; }
    }
}
