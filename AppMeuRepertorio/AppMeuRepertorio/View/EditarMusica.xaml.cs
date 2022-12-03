using AppMeuRepertorio.Model;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMeuRepertorio.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditarMusica : ContentPage
    {
        public EditarMusica()
        {
            InitializeComponent();
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                
                Musica musica_anexada = BindingContext as Musica;


                Musica p = new Musica
                {
                    Id = musica_anexada.Id,
                    Titulo = txt_titulo.Text,
                    Tom = txt_tom.Text,
                    Letra = txt_letra.Text,
                };


                await App.Database.Update(p);

                await DisplayAlert("Sucesso!", "Música Editada", "OK");

                await Navigation.PushAsync(new Listagem());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }
    }
}