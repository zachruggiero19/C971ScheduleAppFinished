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
            ObjectiveAssessmentView.ItemsSource = await DataBaseService.GetObjAssessment(_selectedCourseId);
            PerformanceAssessmentView.ItemsSource = await DataBaseService.GetPerfAssessment(_selectedCourseId);
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
            var assessment = (ObjectiveAssessment)e.CurrentSelection.FirstOrDefault();
            if (e.CurrentSelection != null)
            {
                await Navigation.PushAsync(new ObjectiveAssessmentEdit(assessment));
            }
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
            var courseId = Int32.Parse(_selectedCourseId.ToString());
            await Navigation.PushAsync(new PerformanceAssessmentAdd(courseId));

        }

        async void AddObjAssessment_Clicked(object sender, EventArgs e)
        {
            var courseId = Int32.Parse(_selectedCourseId.ToString());
            await Navigation.PushAsync(new ObjectiveAssessmentAdd(courseId));
        }

        async void Home_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }

    }
}