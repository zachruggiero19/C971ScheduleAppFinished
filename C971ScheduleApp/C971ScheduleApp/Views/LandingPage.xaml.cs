using C971ScheduleApp.TermsCourses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using C971ScheduleApp.Service;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.LocalNotifications;

namespace C971ScheduleApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LandingPage : ContentPage
    {

        //These methods and loops create the notifications for Course Starts and Assessment Starts.
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var courseList = await DataBaseService.GetCourse();
            var objAssessmentList = await DataBaseService.GetObjAssessment();
            var perfAssessmentList = await DataBaseService.GetPerfAssessment();
            var notifyRandom = new Random();
            var notifyId = notifyRandom.Next(1000);

            foreach (Course courseRecord in courseList)
            {
                if (courseRecord.courseNotify == true)
                {
                    if (courseRecord.courseStart == DateTime.Today)
                    {
                        CrossLocalNotifications.Current.Show("Notice", $"{courseRecord.courseName} begins today!", notifyId);
                    }
                }
            }

            foreach (Course courseRecord in courseList)
            {
                if (courseRecord.courseNotify == true)
                {
                    if (courseRecord.courseEnd == DateTime.Today)
                    {
                        CrossLocalNotifications.Current.Show("Notice", $"{courseRecord.courseName} ends today!", notifyId);
                    }
                }
            }

            foreach (ObjectiveAssessment assessment in objAssessmentList)
            {
                if (assessment.objAssessmentNotification == true)
                {
                    if (assessment.startObjAssessment == DateTime.Today)
                    {
                        CrossLocalNotifications.Current.Show("Notice", $"{assessment.objAssessmentName} begins today!", notifyId);
                    }
                }
            }

            foreach (ObjectiveAssessment assessment in objAssessmentList)
            {
                if (assessment.objAssessmentNotification == true)
                {
                    if (assessment.endObjAssessment == DateTime.Today)
                    {
                        CrossLocalNotifications.Current.Show("Notice", $"{assessment.objAssessmentName} ends today!", notifyId);
                    }
                }
            }

            foreach (PerformanceAssessment assessment in perfAssessmentList)
            {
                if (assessment.perfAssessmentNotification == true)
                {
                    if (assessment.startPerfAssessment == DateTime.Today)
                    {
                        CrossLocalNotifications.Current.Show("Notice", $"{assessment.perfAssessmentName} begins today!", notifyId);
                    }
                }
            }

            foreach (PerformanceAssessment assessment in perfAssessmentList)
            {
                if (assessment.perfAssessmentNotification == true)
                {
                    if (assessment.endPerfAssessment == DateTime.Today)
                    {
                        CrossLocalNotifications.Current.Show("Notice", $"{assessment.perfAssessmentName} ends today!", notifyId);
                    }
                }
            }
        }

        public LandingPage()
        {
            InitializeComponent();
        }


        //Adds Terms button
        async void AddTerm_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TermAdd());
        }


        //Leads to View term, term edit, and course + assessment pages
        async void ViewTerm_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TermList());
        }
       
        //Leads to Settings 
        async void Settings_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AppSettings());
        }
    }
}