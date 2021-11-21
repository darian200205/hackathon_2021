using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BusApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new DriverPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
