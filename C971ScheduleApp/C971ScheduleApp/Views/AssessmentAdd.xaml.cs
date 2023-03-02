using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C971ScheduleApp.Service;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace C971ScheduleApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ObjectiveAssessmentAdd : ContentPage
    {
        int _selectedCourseId;
        public ObjectiveAssessmentAdd(int courseId)
        {
            InitializeComponent();
            _selectedCourseId = courseId;
            
        }
        public ObjectiveAssessmentAdd()
        {
            InitializeComponent();
        }

        async void ObjAddSave_Clicked(object sender, EventArgs e)
        {
            var paCheck = DataBaseService.GetPACount(_selectedCourseId);
            var oaCheck = DataBaseService.GetOACount(_selectedCourseId);

            if (string.IsNullOrWhiteSpace(AssessmentName.Text))
            {
                await DisplayAlert("Missing Assessment Name", "Please Enter a Name", "Ok");
                return;
            }
            if (StartDate.Date > EndDate.Date)
            {
                await DisplayAlert("Enter a Starting Date before End date", "Enter an appropriate Start or end Time", "OK");
                return;
            }
            if (await paCheck == 1 && AssessmentType.SelectedItem.ToString() == "Performance Assessment")
            {
                await DisplayAlert("You already have that type of assessment.", "Please delete the matching assessment", "Ok");
                return;
            }
            if (await oaCheck == 1 && AssessmentType.SelectedItem.ToString() == "Objective Assessment")
            {
                await DisplayAlert("You already have that type of assessment.", "Please delete the matching assessment", "Ok");
                return;
            }


            await DataBaseService.AddAssessment(_selectedCourseId, AssessmentName.Text, AssessmentType.SelectedItem.ToString(),
                                           Notification.IsToggled, StartDate.Date, EndDate.Date);
                await Navigation.PopAsync();
            

               
            

        }

        async void ObjAddCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}