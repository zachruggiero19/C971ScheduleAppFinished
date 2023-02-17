using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C971ScheduleApp.TermsCourses;
using C971ScheduleApp.Service;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace C971ScheduleApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CourseEdit : ContentPage
    {
        private readonly int _selectedCourseId;

        public CourseEdit(Course course)
        {
            InitializeComponent();

            _selectedCourseId = course.cId;

            CourseId.Text = course.cId.ToString();
            CourseName.Text = course.courseName;
            StartDate.Date = course.courseStart;
            EndDate.Date = course.courseEnd;
            CourseStatus.SelectedItem = course.courseStatus;
            NoteEditor.Text = course.courseNotes;
            Notification.IsToggled = course.courseNotify;
            InstructorName.Text = course.instructorName;
            InstructorPhone.Text = course.instructorPhone.ToString();
            InstructorEmail.Text = course.instructorEmail;
        }
        public CourseEdit()
        {
            InitializeComponent();
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

            await DataBaseService.AddCourse(Int32.Parse(CourseId.Text), CourseName.Text, StartDate.Date, EndDate.Date,
                    CourseStatus.SelectedItem.ToString(),
                     NoteEditor.Text, Notification.IsToggled, InstructorName.Text,
                    Int32.Parse(InstructorPhone.Text), InstructorEmail.Text);

            await Navigation.PopAsync();
        }

        async void CancelCourse_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        async void DeleteCourse_Clicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Delete this Course and its related Assessments?", "Delete this Course?", "Yes", "No");

            if (answer == true)
            {
                var id = int.Parse(CourseId.Text);
                await DataBaseService.DeleteCourse(id);

                await DisplayAlert("Course Deleted", "Course Deleted", "Ok");
            }
        }

        async void ShareButton_Clicked(object sender, EventArgs e)
        {
            var text = CourseName.Text;
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = text,
                Title = "Share Text"
            });
        }

        async void ShareUri_Clicked(object sender, EventArgs e)
        {
            string uri = "https://www.google.com";
            await Share.RequestAsync(new ShareTextRequest
            {
                Uri = uri,
                Title = "Share Web Link"
            });
        }



        async void AddObjAssessment_Clicked(object sender, EventArgs e)
        {
            var courseId = Int32.Parse(CourseId.Text);
            await Navigation.PushAsync(new ObjectiveAssessmentAdd(courseId));
        }

        async void PerformanceAssessmentView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var assessment = (PerformanceAssessment)e.CurrentSelection.FirstOrDefault();
            if (e.CurrentSelection != null)
            {
                await Navigation.PushAsync(new PerformanceAssessmentEdit(assessment));
            }
        }

        async void AddPerfAssessment_Clicked(object sender, EventArgs e)
        {
            var courseId = Int32.Parse(CourseId.Text);
            await Navigation.PushAsync(new PerformanceAssessmentAdd(courseId));
        }

        async void ObjectiveAssessmentView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var assessment = (ObjectiveAssessment)e.CurrentSelection.FirstOrDefault();
            if (e.CurrentSelection != null)
            {
                await Navigation.PushAsync(new ObjectiveAssessmentEdit(assessment));
            }
        }

        async void AssessmentViewButton_Clicked(object sender, EventArgs e)
        {
            var courseId = Int32.Parse(CourseId.Text);
            await Navigation.PushAsync(new AssessmentView(courseId));
        }
    }
}