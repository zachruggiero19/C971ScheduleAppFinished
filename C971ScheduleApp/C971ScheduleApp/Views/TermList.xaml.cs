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
    public partial class TermList : ContentPage
    {
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            TermCollectionView.ItemsSource = await DataBaseService.GetTerm();
        }
        public TermList()
        {
            InitializeComponent();
        }

        private async void TermCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                Term term = (Term)e.CurrentSelection.FirstOrDefault();
                await Navigation.PushAsync(new TermEdit(term));
            }
        }
    }
}