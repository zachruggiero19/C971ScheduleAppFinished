using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace C971ScheduleApp.TermsCourses
{
    public class Course
    {
        [PrimaryKey, AutoIncrement]
        public int cId { get; set; }
        public int termId { get; set; }
        public string courseName { get; set; }
        public DateTime courseStart { get; set; } 
        public DateTime courseEnd { get; set; }
        public string courseStatus { get; set; }
        public string courseNotes { get; set; }
        public bool courseNotify { get; set; }
        public string instructorName { get; set; }
        public int instructorPhone { get; set;}
        public string instructorEmail { get; set; }
    }
}
