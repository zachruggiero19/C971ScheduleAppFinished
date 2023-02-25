using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using C971ScheduleApp.TermsCourses;
using C971ScheduleApp.Service;
using System.Linq;

namespace C971ScheduleApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermEdit : ContentPage
    {
        private readonly int _selectedTermId;
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            int countCourses = await DataBaseService.GetCourseCountAsync(_selectedTermId);
            CountLabel.Text = countCourses.ToString();

            CourseCollectionView.ItemsSource = await DataBaseService.GetCourse(_selectedTermId);
        }
        public TermEdit()
        {
            InitializeComponent();
        }
        public TermEdit(Term term)
        {
            InitializeComponent();

            _selectedTermId = term.Id;

            TermId.Text = term.Id.ToString();
            TermName.Text = term.termName;
            StartDate.Date = term.startDate;
            EndDate.Date = term.endDate;
        }

        async void SaveTerm_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TermName.Text))
            {
                await DisplayAlert("Missing Name", "Please Enter a name.", "OK");
                return;
            }
            if (StartDate.Date > EndDate.Date)
            {
                await DisplayAlert("Please pick valid times", "Enter a Start Date before end date ", "OK");
                return;
            }

            await DataBaseService.UpdateTerm(Int32.Parse(TermId.Text), TermName.Text, DateTime.Parse(StartDate.Date.ToString()), DateTime.Parse(EndDate.Date.ToString()));
        }

        async void CancelTerm_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        async void DeleteTerm_Clicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Delete this term and its related courses?", "Delete this term?", "Yes", "No");

            if (answer == true)
            {
                var id = int.Parse(TermId.Text);
                await DataBaseService.DeleteTerm(id);

                await DisplayAlert("Term Deleted", "Term Deleted", "Ok");
            }
            await Navigation.PopAsync();
        }

        async void AddCourse_Clicked(object sender, EventArgs e)
        {
            if (Int32.Parse(CountLabel.Text) == 6)
            {
                await DisplayAlert("You can only have six Courses", "Please Delete a course to continue", "OK");
                return;
            }
            var termId = Int32.Parse(TermId.Text);
            await Navigation.PushAsync(new CourseAdd(termId));
        }

        async void CourseCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var course = (Course)e.CurrentSelection.FirstOrDefault();
            if (e.CurrentSelection != null)
            {
                await Navigation.PushAsync(new CourseEdit(course));
            }
        }

    }
}