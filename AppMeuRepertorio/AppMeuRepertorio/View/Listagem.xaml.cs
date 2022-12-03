using AppMeuRepertorio.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMeuRepertorio.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Listagem : ContentPage
    {
     
        ObservableCollection<Musica> lista_musicas = new ObservableCollection<Musica>();


        
        public Listagem()
        {
            InitializeComponent();

            lst_musicas.ItemsSource = lista_musicas;
        }

 
        private void ToolbarItem_Clicked_Novo(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushAsync(new AddMusica());

            } catch(Exception ex)
            {
                DisplayAlert("Ops", ex.Message, "OK");
            }            
        }

        protected override void OnAppearing()
        {
         
            if (lista_musicas.Count == 0)
            {
                
                Task.Run(async () =>
                {
                  
                    List<Musica> temp = await App.Database.GetAll();

                    foreach (Musica item in temp)
                    {
                        lista_musicas.Add(item);
                    }
                    
                });
            }                       
        }

        private void lst_musicas_Refreshing(object sender, EventArgs e)
        {
            lista_musicas.Clear();

            Task.Run(async () =>
            {
                List<Musica> temp = await App.Database.GetAll();

                foreach (Musica item in temp)
                {
                    lista_musicas.Add(item);
                }

            });

            ref_carregando.IsRefreshing = false;

        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
           
            MenuItem disparador = (MenuItem)sender;


            Musica musica_selecionada = (Musica)disparador.BindingContext;
            
 
            bool confirmacao = await DisplayAlert("Tem Certeza?", "Remover Item?", "Sim", "Não");

            if(confirmacao)
            {
                
                await App.Database.Delete(musica_selecionada.Id);

                
                lista_musicas.Remove(musica_selecionada);
            }
        }


        private void txt_busca_TextChanged(object sender, TextChangedEventArgs e)
        {
            string buscou = e.NewTextValue;

            System.Threading.Tasks.Task.Run(async () =>
            {
                List<Musica> temp = await App.Database.Search(buscou);

                
                lista_musicas.Clear();

                foreach (Musica item in temp)
                {
                    lista_musicas.Add(item);
                }

                ref_carregando.IsRefreshing = false;
            });
        }


        private void lst_musicas_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if(e.SelectedItem != null)
            {
                Musica musica_selecionada = (Musica)e.SelectedItem;

                // lst_musicas.SelectedItem = null;

                ((ListView)sender).SelectedItem = null;

                Navigation.PushAsync(new EditarMusica
                {
                    BindingContext = musica_selecionada
                });

            }            
        }
    }
}