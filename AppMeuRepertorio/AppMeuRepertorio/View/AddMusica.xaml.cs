using AppMeuRepertorio.Model;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMeuRepertorio.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddMusica : ContentPage
    {
        public AddMusica()
        {
            InitializeComponent();
        }

       
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
              
                Musica m = new Musica
                {
                    Titulo = txt_titulo.Text,
                    Tom = txt_tom.Text,
                    Letra = txt_letra.Text,
                };


                await App.Database.Insert(m);

 
                await DisplayAlert("Sucesso!", "Música Cadastrada", "OK");


                //await Navigation.PushAsync(new Listagem());
                App.Current.MainPage = new NavigationPage(new Listagem());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }
    }
}