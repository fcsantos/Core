using ProjetoMRP.Paciente.Services;
using ProjetoMRP.Paciente.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjetoMRP.Paciente.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        public Guid userId;
        public Home()
        {
            userId = Guid.Parse(Application.Current.Properties["UserId"].ToString());

            if (userId==null)
                Navigation.PushAsync(new LoginPage());

            InitializeComponent();
            var dateTime = DateTime.Now.Hour;

            var userName = string.Empty;

            if (!string.IsNullOrEmpty(Application.Current.Properties["UserName"].ToString()))
            {
                userName = ", " + Application.Current.Properties["UserName"].ToString();
            }

            if (dateTime >= 0 && dateTime <= 11)
            {
                lblCliente.Text = "Bom Dia" + userName;
            }
            else if (dateTime >= 12 && dateTime <= 17)
            {
                lblCliente.Text = "Boa Tarde" + userName;
            }
            else if (dateTime >= 18 && dateTime <= 23)
            {
                lblCliente.Text = "Boa Noite" + userName;
            }
            else
            {
                lblCliente.Text = "Bem Vindo" + userName;
            }

            CarregarMsgHome(userId);
        }
        #region Métodos Home
        private async void CarregarMsgHome(Guid userId)
        {
            var dataPlanoCuidados = new HomeViewModel.Menu();
            var dataInqueritos = new HomeViewModel.Menu();
            var dataMensagens = new HomeViewModel.Menu();

            var planocuidados = await ApiService.GetList<PlanoCuidado>("careplans/get-all-careplans-bypatient");
            var inqueritos = await ApiService.GetList<InquiryScheduleViewModel>("inquiries-schedule/get-all-bypatient-answered-or-not/false");
            var mensagens = await ApiService.GetList<Mensagens>("messages/get-all-messages-bypatient");


            dataPlanoCuidados.Text = $"Possui {planocuidados.Count()} plano(s) de cuidado(s).";
            dataPlanoCuidados.NumberOf = "Tem alterações no plano de cuidados.";
            dataPlanoCuidados.Image = "HealthCare_80.png";
            dataPlanoCuidados.Page = "PlanoCuidados";
            

            dataInqueritos.Text = $"Possui {inqueritos.Count()} inquérito(s).";
            dataInqueritos.NumberOf = $"Tem {inqueritos.Count()} inquéritos para preencher.";
            dataInqueritos.Image = "Inquerito_80.png";
            dataInqueritos.Page = "InqueritosPage";

            dataMensagens.Text = $"Possui {mensagens.Count()} mensagem(s)."; ;
            dataMensagens.NumberOf = $"Tem {mensagens.Count()} mensagens por ler.";
            dataMensagens.Image = "Mensagens_80.png";
            dataMensagens.Page = "MensagemPage";

            var model = new HomeViewModel();
            model.MenuItems.Add(dataPlanoCuidados);
            model.MenuItems.Add(dataInqueritos);
            model.MenuItems.Add(dataMensagens);
            
            menu.BindingContext = model;

        }
        #endregion

        #region Eventos

        private async void btnInqueritos_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new InqueritosPage());
        }

        private async void btnMessagens_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new MensagemPage());
        }

        private async void btnAjuda_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new AjudaPage());
        }

        #endregion

        private async void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (menu.SelectedItem != null)
            {
                string MenuPageSelected = (e.CurrentSelection.FirstOrDefault() as HomeViewModel.Menu)?.Page;
                System.Reflection.Assembly AppNamespace = System.Reflection.Assembly.GetExecutingAssembly();
                var GetPage = Type.GetType($"{AppNamespace.GetName().Name}.View.{MenuPageSelected}");
                Page page = Activator.CreateInstance(GetPage) as Page;
                await Application.Current.MainPage.Navigation.PushAsync(page);
                menu.SelectedItem = null;
            }
        }
    }
}