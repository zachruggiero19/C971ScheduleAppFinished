using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace C971ScheduleApp.Service
{
    public static class CourseSettings
    {
        public static bool FirstRun
        {
            get => Preferences.Get(nameof(FirstRun), true);
            set => Preferences.Set(nameof(FirstRun), value);
        }
    }
}
