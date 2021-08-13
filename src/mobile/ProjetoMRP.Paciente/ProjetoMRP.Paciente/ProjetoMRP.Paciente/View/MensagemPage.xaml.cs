using ProjetoMRP.Paciente.Services;
using ProjetoMRP.Paciente.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjetoMRP.Paciente.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MensagemPage : ContentPage
    {
        ViewCell lastCell;
        public MensagemPage()
        {
            InitializeComponent();
            CarregarListaMensagens();
            lstMensagens.IsVisible = true;
        }
        private async void CarregarListaMensagens()
        {
            var dataSource = await ApiService.GetList<Mensagens>("messages/get-all-messages-bypatient");

            foreach (var item in dataSource)
            {
                if (item.StatusMessage != "AwaitingResponse")
                {
                    item.Color = Color.Green;
                }
                else
                {
                    item.Color = Color.Red;
                }
            }
            lstMensagens.ItemsSource = dataSource.OrderByDescending(msg=> msg.DateFormat);
        }

        private async void lstMensagens_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Mensagens mensagem = lstMensagens.SelectedItem as Mensagens;

            if (mensagem.StatusMessage != "Sent")
            {
                await Navigation.PushAsync(new RespostaPage(mensagem));
                //var page = new RespostaPage(mensagem);
                //lstMensagens.IsVisible = false;

            }
        }

        private void ViewCell_Tapped(object sender, System.EventArgs e)
        {
            if (lastCell != null)
                lastCell.View.BackgroundColor = Color.Transparent;
            var viewCell = (ViewCell)sender;

            if (viewCell.View != null)
            {
                viewCell.View.BackgroundColor = Color.AntiqueWhite;
                lastCell = viewCell;
            }
        }
    }
}