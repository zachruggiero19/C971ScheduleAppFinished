using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using C971ScheduleApp.Service;
using C971ScheduleApp.TermsCourses;

namespace C971ScheduleApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermAdd : ContentPage
    {
        public TermAdd()
        {
            InitializeComponent();
        }

        async void CancelTerm_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
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

            await DataBaseService.AddTerm( TermName.Text, DateTime.Parse(StartDate.Date.ToString()), DateTime.Parse(EndDate.Date.ToString()));

            await Navigation.PopAsync();
        }
    }
}