using ProjetoMRP.Paciente.Services;
using ProjetoMRP.Paciente.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using Xamarin.Forms.Xaml;

namespace ProjetoMRP.Paciente.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AjudaPage : ContentPage
    {
        public AjudaPage()
        {
            InitializeComponent();
            CarregarDadosAjuda();
        }
        private async void CarregarDadosAjuda()
        {
            var response = await ApiService.GetList<AjudaViewModel>("EmergencyChannels");
            collectionViewListHorizontal.ItemsSource = response.OrderBy(item => item.sortOrder);
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var itemTapped = (PancakeView)sender;
            var ajuda = (AjudaViewModel)itemTapped.BindingContext;
            CallTo(ajuda.Cell);
            //Launcher.OpenAsync("tel:" + ajuda.Cell);
        }

        public void CallTo(string number)
        {
            try
            {
                PhoneDialer.Open(number);
            }
            catch (ArgumentNullException anEx)
            {
                App.Current.MainPage.DisplayAlert("Erro!", anEx.Message, "Ok");
            }
            catch (FeatureNotSupportedException ex)
            {
                App.Current.MainPage.DisplayAlert(ex.Message, "Phone Dialer is not supported on this device.", "Ok");
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("Erro!", ex.Message, "Ok");
            }
        }

    }
}