using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace C971ScheduleApp.TermsCourses
{
    public class Assessment
    {
        [PrimaryKey, AutoIncrement]
        public int AssessmentId { get; set; }
        public int courseId { get; set; }
        public string AssessmentName { get; set; }
        public string AssessmentType { get; set; }
        public bool AssessmentNotification { get; set; }
        public DateTime startAssessment { get; set; }
        public DateTime endAssessment { get; set; }
    }
}
