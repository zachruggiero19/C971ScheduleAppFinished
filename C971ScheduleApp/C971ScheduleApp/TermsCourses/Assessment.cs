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
        public string objAssessmentName { get; set; }
        public string objAssessemntType { get; set; }
        public bool objAssessmentNotification { get; set; }
        public DateTime startObjAssessment { get; set; }
        public DateTime endObjAssessment { get; set; }
    }
}
