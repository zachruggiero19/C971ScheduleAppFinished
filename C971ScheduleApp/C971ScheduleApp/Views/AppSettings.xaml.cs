using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C971ScheduleApp.Service;
using C971ScheduleApp.TermsCourses;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace C971ScheduleApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppSettings : ContentPage
    {
        public AppSettings()
        {
            InitializeComponent();
        }

        private void ClearPreferences_Clicked(object sender, EventArgs e)
        {
            Preferences.Clear();
        }

        async void ClearSampleData_Clicked(object sender, EventArgs e)
        {
            await DataBaseService.ClearSampleData();
        }

        async void LoadSampleData_Clicked(object sender, EventArgs e)
        {
            if (CourseSettings.FirstRun)
            {
                DataBaseService.LoadSampleData();
                CourseSettings.FirstRun = false;

                await Navigation.PopToRootAsync();
            }
        }
    }
}