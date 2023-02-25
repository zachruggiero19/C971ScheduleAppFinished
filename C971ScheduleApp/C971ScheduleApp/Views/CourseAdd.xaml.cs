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
    public partial class CourseAdd : ContentPage
    {
        int _selectedTermId;
        public CourseAdd(int termId)
        {
            InitializeComponent();
            _selectedTermId = termId;
          
            
        }
        public CourseAdd()
        {
            InitializeComponent();
        }

        async void Home_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }

       async void CancelCourse_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        async void SaveCourse_Clicked(object sender, EventArgs e)
        {
            int tossedInt;

            if (string.IsNullOrWhiteSpace(CourseName.Text))
            {
                await DisplayAlert("Missing Name", "Please Enter a Name", "Ok");
                return;
            }
            if (string.IsNullOrWhiteSpace(CourseStatus.SelectedItem.ToString()))
            {
                await DisplayAlert("Missing Course Status", "Please Select a Course Status", "Ok");
                return;
            }
            if (string.IsNullOrWhiteSpace(InstructorName.Text))
            {
                await DisplayAlert("Missing Instructor Name", "Please Enter an Instructor Name", "Ok");
                return;
            }
            if (!Int32.TryParse(InstructorPhone.Text, out tossedInt))
            {
                await DisplayAlert("Please enter a 6 digit Phone number with no spaces", "Please Enter a phone number", "Ok");
                return;
            }
            if (string.IsNullOrWhiteSpace(InstructorEmail.Text))
            {
                await DisplayAlert("Missing Instructor Email", "Please Enter an Instructor Email", "Ok");
                return;
            }

            else
            {
                await DataBaseService.AddCourse(_selectedTermId, CourseName.Text, StartDate.Date, EndDate.Date,
                   CourseStatus.SelectedItem.ToString(),
                    NoteEditor.Text, Notification.IsToggled, InstructorName.Text,
                   Int32.Parse(InstructorPhone.Text), InstructorEmail.Text);
            }
                        

           

            await Navigation.PopAsync();

        }
    }
}