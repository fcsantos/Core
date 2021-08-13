using ProjetoMRP.Paciente.Services;
using ProjetoMRP.Paciente.Utilities.Load;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace ProjetoMRP.Paciente.ViewModel
{
    public class LoginViewModel
    {
        public Command LoginCommand { get; set; }
        public INavigation Navigation { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        public LoginViewModel(INavigation nav, string txtusername, string txtpassword)
        {
            LoginCommand = new Command(LoginCommandAction);
            Navigation = nav;
#if DEBUG
            username = "pacient1@paciente.com";
            password = "Default@123456";
#endif

            username = txtusername;
            password = txtpassword;

        }

        private async void LoginCommandAction()
        {
            await Navigation.PushPopupAsync(new Loading());
            try
            {
                Application.Current.Properties["UserId"] = "";
                var auth = new ApiService();

                var authVm = new AuthenticationViewModel
                {
                    Email = username,
                    Password = password
                };

                if (authVm.Email == null || authVm.Password == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", "Preencha os seus dados.", "OK");

                }
                else
                {
                    auth.Login(authVm).Wait();

                    if (Application.Current.Properties != null && Application.Current.Properties["UserId"].ToString() != "")
                        Application.Current.MainPage = new NavigationPage(new MainPage());
                    else
                        await Application.Current.MainPage.DisplayAlert("Erro", "Login invalido", "OK");
                }

            }
            catch (System.Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Falha de Ligação", ex.ToString(), "Ok");
            }
            await Navigation.PopAllPopupAsync();
        }
    }

    public class AuthenticationViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}