using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TeamManagement.Xam.Form.Services;
using TeamManagement.Xam.Form.Views;

namespace TeamManagement.Xam.Form
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
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
