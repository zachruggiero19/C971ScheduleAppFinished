using C971ScheduleApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using C971ScheduleApp.TermsCourses;
using C971ScheduleApp.Service;

namespace C971ScheduleApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            if (CourseSettings.FirstRun)
            {
               

                DataBaseService.LoadSampleData();
               //DataBaseService.LoadSampleDataSql();

                CourseSettings.FirstRun = false;
            }

            var landingPage = new LandingPage();
            var navPage = new NavigationPage(landingPage);
            MainPage = navPage;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
