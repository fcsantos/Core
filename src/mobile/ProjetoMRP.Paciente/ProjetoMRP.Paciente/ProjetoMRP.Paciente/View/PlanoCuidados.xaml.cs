using ProjetoMRP.Paciente.Services;
using ProjetoMRP.Paciente.ViewModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjetoMRP.Paciente.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    
    public partial class PlanoCuidados : ContentPage
    {
        public PlanoCuidados()
        {
            InitializeComponent();
            CarregarListaPlanoCuidados();
        }

        private async void CarregarListaPlanoCuidados()
        {
            var response = await ApiService.GetList<PlanoCuidado>("careplans/get-all-careplans-bypatient");

            if (response != null)
            {
                lst.ItemsSource = response.OrderBy(item => item.DateFormat); ;
            }
        }
    }
}