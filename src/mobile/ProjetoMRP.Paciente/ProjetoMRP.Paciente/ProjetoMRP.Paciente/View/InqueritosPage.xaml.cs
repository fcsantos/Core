using ProjetoMRP.Paciente.Services;
using ProjetoMRP.Paciente.ViewModel;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjetoMRP.Paciente.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class InqueritosPage : ContentPage
    {
        ViewCell lastCell;
        InquiryScheduleViewModel inquerito;
        public InqueritosPage()
        {
            InitializeComponent();
            CarregarListaInqueritos();
        }

        private async void CarregarListaInqueritos()
        {
            var list = await ApiService.GetList<InquiryScheduleViewModel>("inquiries-schedule/get-all-bypatient-answered-or-not/false");
            lstInqueritos.ItemsSource = list.Where(ii => Convert.ToDateTime(ii.StartDate.ToShortDateString()) <= Convert.ToDateTime(DateTime.Now.ToShortDateString())).OrderByDescending(inq => inq.StartDate.ToShortDateString());
        }

        private async void itemTapped(object sender, ItemTappedEventArgs e)
        {
            inquerito = e.Item as InquiryScheduleViewModel;

            if (Convert.ToDateTime(inquerito.StartDate.ToShortDateString()) > Convert.ToDateTime(DateTime.Now.ToShortDateString()) && inquerito.answered == false)
            {
                await Application.Current.MainPage.DisplayAlert("Responder", "Este inquérito nao pode ser respondido.", "OK");
            }
            else
            {                
                await Navigation.PushAsync(new InqueritoFormPage(inquerito));
            }
        }

    }
}