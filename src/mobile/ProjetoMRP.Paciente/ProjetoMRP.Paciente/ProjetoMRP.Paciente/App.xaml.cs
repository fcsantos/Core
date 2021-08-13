using ProjetoMRP.Paciente.View;
using Xamarin.Forms;
using Com.OneSignal;
using Com.OneSignal.Abstractions;
using System.Collections.Generic;

namespace ProjetoMRP.Paciente
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Device.SetFlags(new string[] { "Expander_Experimental" });
            //Device.SetFlags(new string[] { "RadioButton_Experimental" });
            OneSignal.Current.StartInit("eda1f047-2102-4968-a61e-104de373bc24").EndInit();

            if (Application.Current.Properties.Count != 0)
                Application.Current.MainPage = new NavigationPage(new MainPage());
            else
                Application.Current.MainPage = new NavigationPage(new LoginPage());

        }

        protected override void OnStart()
        {
            OneSignal.Current.RegisterForPushNotifications();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
