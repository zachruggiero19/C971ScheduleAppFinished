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
    public partial class PerformanceAssessmentEdit : ContentPage
    {
        private readonly int _selectedAssessmentId;
        public PerformanceAssessmentEdit(PerformanceAssessment assessment)
        {
            InitializeComponent();
            _selectedAssessmentId = assessment.perfAssessmentId;

            AssessmentId.Text = assessment.perfAssessmentId.ToString();
            AssessmentName.Text = assessment.perfAssessmentName;
            StartDate.Date = assessment.startPerfAssessment;
            EndDate.Date = assessment.endPerfAssessment;
            Notification.IsToggled = assessment.perfAssessmentNotification;
        }
        public PerformanceAssessmentEdit()
        {
            InitializeComponent();
        }

        async void SavePerformanceAssessment_Clicked(object sender, EventArgs e)
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
            await DataBaseService.UpdatePerfAssessment(_selectedAssessmentId, AssessmentName.Text, AssessmentType.Text,
                                                    Notification.IsToggled, StartDate.Date, EndDate.Date);
        }

        async void DeletePerfAssessment_Clicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Delete this Assessment?", "Delete this Assessment?", "Yes", "No");

            if (answer == true)
            {
                var id = int.Parse(AssessmentId.Text);
                await DataBaseService.DeleteCourse(id);

                await DisplayAlert("Objective Assessment Deleted", " Objective Assessment Deleted", "Ok");
            }
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}