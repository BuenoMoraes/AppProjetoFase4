using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppProjetoFase4.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageAddLivro : ContentPage
    {
        public PageAddLivro()
        {
            InitializeComponent();
        }

        private async void btnAddLivro_Clicked(object sender, EventArgs e)
        {
            int status, autor = 0;
        

            using (var httpClient = new HttpClient())
            {
                //Verificação do status, transformando escolhido na tela no id do status
                String iPickerStatus = (String)pkStatus.SelectedItem;
                
                if (iPickerStatus == "Disponível")
                {
                    status = 2;
                }
                else
                {
                   status = 1;
                }

                //Verificação do autor, transformando escolhido na tela no id do autor
                String iPickerAutor = (String)pkAutor.SelectedItem;

                if (iPickerAutor == "Fred Brooks")
                {
                    autor = 1;
                }
                else if(iPickerAutor == "Paulo Coelho")
                {
                    autor = 2;
                }
                else if (iPickerAutor == "Dustin Boswell")
                {
                    autor = 3;
                }
                else if (iPickerAutor == "Robert C. Martin")
                {
                    autor = 4;
                }
                else if (iPickerAutor == "Bob Martin")
                {
                    autor = 5;
                }
                else if (iPickerAutor == "J. K. Rowling")
                {
                    autor = 6;
                }

                Console.WriteLine("Id status: " + status);
                Console.WriteLine("Id autor: " +autor);
                var camposFormulario = new FormUrlEncodedContent(new[]
                {
                        new KeyValuePair<string, string>("titulo", enTitulo.Text),
                        new KeyValuePair<string, string>("autor_id", Convert.ToString(autor)),
                        new KeyValuePair<string, string>("anoPublicacao",enAnoPublicacao.Text),
                        new KeyValuePair<string, string>("status_id", Convert.ToString(status))
                    });
                var httpResponseMessage = await httpClient.PostAsync("http://192.168.0.47:8000/api/livros", camposFormulario);
                Console.WriteLine(httpResponseMessage);
                Console.WriteLine(camposFormulario);
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    await DisplayAlert("Livro cadastrado", "Livro cadastrado com sucesso", "Ok");
                    await Navigation.PushAsync(new ListagemMain()); 
                }
                else
                {
                    await DisplayAlert("Livro Incorreto", "Verifique os dados e tente novamente", "Voltar");
                }
            }

        }
    }
}