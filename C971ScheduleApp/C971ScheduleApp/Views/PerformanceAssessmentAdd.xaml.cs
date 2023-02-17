using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C971ScheduleApp.Service;
using C971ScheduleApp.TermsCourses;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace C971ScheduleApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PerformanceAssessmentAdd : ContentPage
    {
        int _selectedCourseId;
        public PerformanceAssessmentAdd(int courseId)
        {
            InitializeComponent();
            _selectedCourseId = courseId;
        }
        public PerformanceAssessmentAdd()
        {
            InitializeComponent();
        }

        async void PerfAddSave_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(AssessmentName.Text))
            {
                await DisplayAlert("Missing Assessment Name", "Please Enter a Name", "Ok");
                return;
            }
            if (StartDate.Date > EndDate.Date)
            {
                await DisplayAlert("Enter a Starting Date before End date", "Enter an appropriate Start or end Time", "OK");
            }

            await DataBaseService.AddPerfAssessment(_selectedCourseId, AssessmentName.Text, AssessmentType.Text, 
                                                     Notification.IsToggled, StartDate.Date, EndDate.Date);
            await Navigation.PopAsync();
        }

        async void PerfAddCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}