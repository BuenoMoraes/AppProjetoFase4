using Newtonsoft.Json;
using AppProjetoFase4.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using AppProjetoFase4.Views;


namespace AppProjetoFase4
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

         async void btnAuthLogin_Clicked(object sender, EventArgs e)
         {
            using (var cliente = new HttpClient())
            {
                var camposFormulario = new FormUrlEncodedContent(new[]
               { 
                        new KeyValuePair<string, string>("email",  enUsuario.Text),
                        new KeyValuePair<string, string>("password", enSenha.Text),

                });
                var httpResponse = await cliente.PostAsync("http://192.168.0.47:8000/api/login", camposFormulario);
                Console.WriteLine(httpResponse);


                var contentStream = await httpResponse.Content.ReadAsStreamAsync();

                var streamReader = new StreamReader(contentStream);
                var jsonReader = new JsonTextReader(streamReader);

                JsonSerializer serializer = new JsonSerializer();

                try
                {
                    var teste = serializer.Deserialize<LoginApi>(jsonReader);
                    Console.WriteLine(teste.access_token);
                }
                catch (JsonReaderException)
                {
                    Console.WriteLine("Invalid JSON.");
                }


                if (!httpResponse.IsSuccessStatusCode)
                {
                    //Fazer o tratamento de erro
                    Console.WriteLine("erro");
                }
                else
                {
                    await Navigation.PushAsync(new ListagemViewLivro());
                }
            }
           
         }

        private void btnCadastro_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageCadastro());
        }
    }
    class RegistroJson
    {
        public string email { get; set; }
        public int senha { get; set; }
    }
}
