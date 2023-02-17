using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C971ScheduleApp.TermsCourses;
using C971ScheduleApp.Service;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace C971ScheduleApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ObjectiveAssessmentEdit : ContentPage
    {
        private readonly int _selectedObjAssessmentId;
        public ObjectiveAssessmentEdit(ObjectiveAssessment assessment)
        {
            InitializeComponent();
            _selectedObjAssessmentId = assessment.objAssessmentId;

            AssessmentId.Text = assessment.objAssessmentId.ToString();
            AssessmentName.Text = assessment.objAssessmentName;
            StartDate.Date = assessment.startObjAssessment;
            EndDate.Date = assessment.endObjAssessment;
            Notification.IsToggled = assessment.objAssessmentNotification;
        }

        public ObjectiveAssessmentEdit()
        {
            InitializeComponent();
        }


        async void SaveObjAssessment_Clicked(object sender, EventArgs e)
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
            await DataBaseService.UpdateObjAssessment(_selectedObjAssessmentId, AssessmentName.Text, AssessmentType.Text,
                                                    Notification.IsToggled, StartDate.Date, EndDate.Date);
            await Navigation.PopAsync();
        }


        async void DeleteObjAssessment_Clicked(object sender, EventArgs e)
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