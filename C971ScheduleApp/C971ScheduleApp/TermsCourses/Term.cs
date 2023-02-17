using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace C971ScheduleApp.TermsCourses
{
    public class Term
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string termName { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
    }
}
