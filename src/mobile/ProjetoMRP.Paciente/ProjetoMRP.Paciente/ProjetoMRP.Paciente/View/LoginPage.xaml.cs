using ProjetoMRP.Paciente.Services;
using ProjetoMRP.Paciente.ViewModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjetoMRP.Paciente.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
#if DEBUG
            txtUserName.Text = "cruz@hotmail.com";
            txtPassword.Text = "Default@123456";
#endif

            BindingContext = new LoginViewModel(Navigation,txtUserName.Text,txtPassword.Text);
        }

        private void ForgotPass(object sender, System.EventArgs e)
        {
            Launcher.OpenAsync(new System.Uri("https://mrpdev.lusodata.pt/forgot-password"));
        }
    }
}