using ProjetoMRP.Paciente.Services;
using ProjetoMRP.Paciente.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjetoMRP.Paciente.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RespostaPage : ContentPage
    {
        private readonly Guid _idMensagem;
        private readonly Guid _DoctorId;
        private readonly Guid _PatientId;
        private readonly bool _IsReply;
        public string userId;
        ViewCell lastCell;
        public RespostaPage()
        {
            InitializeComponent();

            userId = Application.Current.Properties["UserId"].ToString();
        }

        public RespostaPage(Mensagens mensagem)
        {
            InitializeComponent();
            this.BindingContext = mensagem;
            CarregarMensagem(mensagem);

            _idMensagem = mensagem.Id;
            _DoctorId = mensagem.DoctorId;
            _PatientId = mensagem.PatientId;
            _IsReply = mensagem.IsReply;
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

        private async void CarregarMensagem(Mensagens mensagem)
        {
         
            var pacienteMensagemResposta = await ObterMensagemResposta_Get(mensagem.Id);

            if ("Answered" == mensagem.StatusMessage)
            {
                Responder.IsVisible = false;
                RespostaPaciente.IsVisible = true;
                DataResposta.Text = pacienteMensagemResposta.DateFormat;
                ConteudoResposta.Text = pacienteMensagemResposta.Text;
            }

        }

        private async Task<Mensagens> ObterMensagemResposta_Get(Guid id)
        {
            var result = await ApiService.GetBy<Mensagens>($"messages/get-by-reply-message-id/{id}");
            return result;
        }

        private async void btnEnviar_Clicked(object sender, EventArgs e)
        {
            Mensagens resposta = new Mensagens();
            resposta.Text = txtResposta.Text;
            resposta.ReplyMessageId = _idMensagem;
            resposta.DoctorId = _DoctorId;
            resposta.PatientId = _PatientId;
            resposta.IsReply = _IsReply;

            var apiResponse = await ApiService.Post<Mensagens>("messages", resposta);

            if (apiResponse != true)
                btnEnviar.IsVisible = false;

            await Application.Current.MainPage.DisplayAlert("Aviso", "Mensagem enviada com sucesso!", "OK");
            await Navigation.PushAsync(new MensagemPage());
        }

    }
}