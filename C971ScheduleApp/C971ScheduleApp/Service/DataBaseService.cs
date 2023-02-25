using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using SQLite;
using C971ScheduleApp.TermsCourses;

namespace C971ScheduleApp.Service
{
    public static class DataBaseService
    {
        #region Database Creation
        private static SQLiteAsyncConnection _db;
        private static SQLiteConnection dbConnection;

        static async Task Init()
        {
            if (_db != null)
            {
                return;
            }
            var dataBasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ClassSchedule.db");

            _db = new SQLiteAsyncConnection(dataBasePath);
            dbConnection = new SQLiteConnection(dataBasePath);

            await _db.CreateTableAsync<Course>();
            await _db.CreateTableAsync<Term>();
            await _db.CreateTableAsync<Assessment>();
        }
        #endregion
        #region Term
        public static async Task AddTerm( string termName, DateTime startDate, DateTime endDate)
        {
            await Init();
            var term = new Term()
            {
                termName = termName,
                startDate = startDate,
                endDate = endDate
            };

            await _db.InsertAsync(term);

            var id = term.Id;
            
        }

        public static async Task DeleteTerm(int id)
        {
            await Init();

            await _db.DeleteAsync<Term>(id);
        }
        
        public static async Task<IEnumerable<Term>> GetTerm()
        {
            await Init();

            var terms = await _db.Table<Term>().ToListAsync();
            return terms;
        }

        public static async Task UpdateTerm(int id, string termName, DateTime startDate, DateTime endDate)
        {
            await Init();

            var termQuery = await _db.Table<Term>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();

            if (termQuery != null)
            {
                termQuery.termName = termName;
                termQuery.startDate = startDate;
                termQuery.endDate = endDate;

                await _db.UpdateAsync(termQuery);
            }
        }
        #endregion Term

        #region Classes
        public static async Task AddCourse(int termId, string courseName, DateTime courseStart, DateTime courseEnd, string courseStatus, string courseNotes, bool courseNotify, string instructorName, int instructorPhone, string instructorEmail)
        {
            await Init();

            var course = new Course
            {
                termId = termId,
                courseName = courseName,
                courseStart = courseStart,
                courseEnd = courseEnd,
                courseStatus = courseStatus,
                courseNotes = courseNotes,
                courseNotify = courseNotify,
                instructorName = instructorName,
                instructorPhone = instructorPhone,
                instructorEmail = instructorEmail
            };

            await _db.InsertAsync(course);
            var id = course.cId;
        }

        public static async Task DeleteCourse(int id)
        {
            await Init();

            await _db.DeleteAsync<Course>(id);
        }

        public static async Task<IEnumerable<Course>> GetCourse(int termId)
        {
            await Init();

            var courses = await _db.Table<Course>().Where(i => i.termId == termId).ToListAsync();
            return courses;
        }

        public static async Task<IEnumerable<Course>> GetCourse()
        {
            await Init();

            var courses = await _db.Table<Course>().ToListAsync();
            return courses;
        }

        public static async Task UpdateCourse(int id, string courseName, DateTime courseStart, DateTime courseEnd, string courseStatus, string courseNotes, bool courseNotify, string instructorName, int instructorPhone, string instructorEmail)
        {
            await Init();

            var courseQuery = await _db.Table<Course>()
                .Where(i => i.cId == id)
                .FirstOrDefaultAsync();

            if (courseQuery != null)
            {
                courseQuery.courseName = courseName;
                courseQuery.courseStart = courseStart;
                courseQuery.courseEnd = courseEnd;
                courseQuery.courseStatus = courseStatus;
                courseQuery.courseNotes = courseNotes;
                courseQuery.courseNotify = courseNotify;
                courseQuery.instructorName = instructorName;
                courseQuery.instructorPhone = instructorPhone;
                courseQuery.instructorEmail = instructorEmail;

                await _db.UpdateAsync(courseQuery);
            }
        }

        #endregion Classes

        #region Assessments
        public static async Task AddAssessment(int courseId, string assessmentName, string assessmentType, bool assessmentNotification, DateTime startAssessment, DateTime endAssessment)
        {
            await Init();

            var assessment = new Assessment()
            {
                courseId =  courseId,
                objAssessmentName = assessmentName,
                objAssessemntType = assessmentType,
                objAssessmentNotification = assessmentNotification,
                startObjAssessment = startAssessment,
                endObjAssessment = endAssessment
            };

            await _db.InsertAsync(assessment);
            var id = assessment.AssessmentId;
        }



        public static async Task DeleteAssessment(int assessmentId)
        {
            await Init();

            await _db.DeleteAsync<Assessment>(assessmentId);
        }
      

        public static async Task<IEnumerable<Assessment>> GetAssessment(int courseId)
        {
            await Init();
            var assessments = await _db.Table<Assessment>().Where(i => i.courseId == courseId).ToListAsync();
            return assessments;
        }

        public static async Task<IEnumerable<Assessment>> GetAssessment()
        {
            await Init();

            var objAssessments = await _db.Table<Assessment>().ToListAsync();
            return objAssessments;
        }



        public static async Task UpdateAssessment(int id, string assessmentName, string assessmentType, bool assessmentNotification, DateTime startAssessment, DateTime endAssessment)
        {
            await Init();

            var assessmentQuery = await _db.Table<Assessment>()
                .Where(i => i.AssessmentId == id)
                .FirstOrDefaultAsync();

                assessmentQuery.objAssessmentName = assessmentName;
                assessmentQuery.objAssessemntType = assessmentType;
                assessmentQuery.objAssessmentNotification = assessmentNotification;
                assessmentQuery.startObjAssessment = startAssessment;
                assessmentQuery.endObjAssessment = endAssessment;
        }


        #endregion Assessments
        #region Load Sample Data 
        public static async void LoadSampleData()
        {
            await Init();

            Term term = new Term
            {
                termName = "Term 1",
                startDate = DateTime.Today.Date,
                endDate = DateTime.Today.AddMonths(4)

            };
            await _db.InsertAsync(term);
            Course course = new Course
            {
                courseName = "Course One",
                courseStart = DateTime.Today.Date,
                courseEnd = DateTime.Today.AddMonths(2),
                courseStatus = "Active",
                courseNotify = true,
                instructorName = "Zachariah M Ruggiero",
                instructorPhone = 5789391,
                instructorEmail = "zruggie@wgu.edu",
                termId = term.Id
            };
            await _db.InsertAsync(course);
            Assessment assessment = new Assessment
            {
                objAssessmentName = "Assessment 1",
                objAssessemntType = "Objective Assessment",
                objAssessmentNotification = true, 
                startObjAssessment = DateTime.Today.AddMonths(1),
                endObjAssessment = DateTime.Today.AddMonths(1).AddDays(1),
                courseId = course.cId
            };
            await _db.InsertAsync(assessment);
            Assessment assessment2 = new Assessment
            {
                objAssessmentName = "Assessment 2",
                objAssessemntType = "Performance Assessment",
                objAssessmentNotification = true,
                startObjAssessment = DateTime.Today.AddMonths(1),
                endObjAssessment = DateTime.Today.AddMonths(1).AddDays(3),
                courseId = course.cId
            };
            await _db.InsertAsync(assessment2);

        }
        #endregion

        #region Clear Sample Data
        public static async Task ClearSampleData()
        {
            await Init();

            await _db.DropTableAsync<Term>();
            await _db.DropTableAsync<Course>();
             await _db.DropTableAsync<Assessment>();
            _db = null;
            dbConnection = null;

        }
        #endregion

        #region Load Sample Sql Data
        public static async void LoadSampleDataSql()
        {
            await Init();

            int lastRowId;

            await _db.ExecuteAsync(@"INSERT INTO Term(TERMNAME, STARTDATE, ENDDATE)
                                    VALUES(?,?,?)", "Term 1", DateTime.Today.Date, DateTime.Today.AddMonths(4));
            lastRowId = await _db.ExecuteScalarAsync<int>("SELECT last_insert_rowid()");

            await _db.ExecuteAsync(@"INSERT INTO Course(COURSENAME, COURSESTART, COURSEEND, COURSESTATUS, 
                                    INSTRUCTORNAME, INSTRUCTORPHONE, INSTRUCTOREMAIL, TERMID) 
                                    VALUES(?,?,?,?,?,?,?,?)", "Course One", DateTime.Today.Date, DateTime.Today.AddMonths(2), "Active", 
                                    "Zachariah M Ruggiero", 5789391, "zruggie@wgu.edu", lastRowId);
            lastRowId = await _db.ExecuteScalarAsync<int>("SELECT last_insert_rowid()");

            await _db.ExecuteAsync(@"INSERT INTO Assessment(ASSEMENTNAME, ASSESSMENTTYPE, STARTASSESSMENT, ENDASSESSMENT, COURSEID)
                                    VALUES(?,?,?,?)", "Assessment 1", "Objective Assessment", 
                                    DateTime.Today.AddDays(25), DateTime.Today.AddDays(26), lastRowId );
            await _db.ExecuteAsync(@"INSERT INTO Assessment(ASSEMENTNAME, ASSESSMENTTYPE, STARTASSESSMENT, ENDASSESSMENT)
                                    VALUES(?,?,?,?)", "Assessment 2", "Performance Assessment",
                                    DateTime.Today.AddDays(27), DateTime.Today.AddDays(28), lastRowId);

        }
        #endregion

        #region Count Determinations
        public static async Task<int> GetCourseCountAsync(int selectedTermId)
        {
            int courseCount = await _db.ExecuteScalarAsync<int>($"SELECT COUNT(*) FROM Course WHERE termId = ?", selectedTermId);

            return courseCount;
        }

        public static async Task<int> GetAssessmentCountAsync(int selectedCourseId)
        {
            int assessmentCount = await _db.ExecuteScalarAsync<int>($"SELECT COUNT(*) FROM Assessment WHERE courseId = ?", selectedCourseId);

            return assessmentCount;
        }
        #endregion

        #region Loop through Tables
        public static async void LoopTermTable()
        {
            await Init();
            var allTermRecords = dbConnection.Query<Term>("SELECT * FROM Term");

            foreach (var termRecord in allTermRecords)
            {
                Console.WriteLine("Term Name: " + termRecord.termName);
            }
        }

        public static async void LoopCourseTable()
        {
            await Init();
            var allCourseRecords = dbConnection.Query<Course>("SELECT * FROM Course");

            foreach (var courseRecord in allCourseRecords)
            {
                Console.WriteLine("Course Number: " + courseRecord.courseName);
            }
        }

        public static async void LoopAssessmentTable()
        {
            await Init();
            var allAssessmentRecords = dbConnection.Query<Assessment>("SELECT * FROM Assessment");

            foreach (var assessmentRecord in allAssessmentRecords)
            {
                Console.WriteLine("Assessment Name: " + assessmentRecord.AssessmentId);
            }
        }

        public static async Task<List<Term>> GetNotifyTermAsync()
        {
            await Init();
            var records = dbConnection.Query<Term>("SELECT * FROM Term");
            return records;
        }

        public static async Task<List<Course>> GetNotifyCourseAsync()
        {
            await Init();
            var records2 = dbConnection.Query<Course>("SELECT * FROM Course");
            return records2;
        }

        public static async Task<List<Assessment>> GetNotifyObjAssessmentAsync()
        {
            await Init();
            var records3 = dbConnection.Query<Assessment>("SELECT * FROM ObjectiveAssessment");
            return records3;
        }


        public static async Task<IEnumerable<Term>> GetNotifyTerm()
        {
            await Init();
            var allTermRecords = dbConnection.Query<Term>("SELECT STARTDATE, ENDDATE FROM Term");

            return allTermRecords;
        }

        public static async Task<IEnumerable<Term>> GetNotifyCourse()
        {
            await Init();
            var allCourseRecords = dbConnection.Query<Term>("SELECT COURSESTART, COURSEEND FROM Course");

            return allCourseRecords;
        }
        #endregion

    }
}
