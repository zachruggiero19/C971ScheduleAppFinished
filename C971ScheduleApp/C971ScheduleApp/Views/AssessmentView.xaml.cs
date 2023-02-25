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
    public partial class AssessmentView : ContentPage
    {
        private readonly int _selectedCourseId;
        protected override async void OnAppearing()
        {
            AssessmentCollectionView.ItemsSource = await DataBaseService.GetAssessment(_selectedCourseId);

            int assessmentCount = await DataBaseService.GetAssessmentCountAsync(_selectedCourseId);
            CountLabel.Text = assessmentCount.ToString();
        
        }
        public AssessmentView(int courseId)
        {
            InitializeComponent();
            _selectedCourseId = courseId;

        }
        public AssessmentView()
        {
            InitializeComponent();
        }

        async void ObjectiveAssessmentView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var assessment = (Assessment)e.CurrentSelection.FirstOrDefault();
            if (e.CurrentSelection != null)
            {
                await Navigation.PushAsync(new ObjectiveAssessmentEdit(assessment));
            }
        }

        async void AddObjAssessment_Clicked(object sender, EventArgs e)
        {
            if (Int32.Parse(CountLabel.Text) == 2)
            {
                await DisplayAlert("You can only have two Assessments", "Please Delete an assessment to continue", "OK");
                return;
            }
            var courseId = Int32.Parse(_selectedCourseId.ToString());
            await Navigation.PushAsync(new ObjectiveAssessmentAdd(courseId));
        }

        async void Home_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }

        async void AssessmentCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                Assessment assessment = (Assessment)e.CurrentSelection.FirstOrDefault();
                await Navigation.PushAsync(new ObjectiveAssessmentEdit(assessment));
            }
        }
    }
}