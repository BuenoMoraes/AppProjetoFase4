using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppProjetoFase4
{
    public partial class PageCadastro : ContentPage
    {
        public PageCadastro()
        {
            InitializeComponent();
        }

        async void btnCadastro_Clicked(object sender, EventArgs e)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromSeconds(200);
                var camposFormulario = new FormUrlEncodedContent(new[]
                {
                        new KeyValuePair<string, string>("name", enNome.Text),
                        new KeyValuePair<string, string>("email", enUsuario.Text),
                        new KeyValuePair<string, string>("password", enSenha.Text)
                    });
                var httpResponseMessage = await httpClient.PostAsync("http://192.168.0.47:8000/api/registros", camposFormulario);
                Console.WriteLine(httpResponseMessage);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    await DisplayAlert("Cadastro realizado", "Cadastro realizado com sucesso", "Ok");
                    await Navigation.PushAsync(new MainPage());
                }
                else
                {
                    await DisplayAlert("Cadastro Incorreto", "Cadastro Incorreto", "Voltar");
                }
            }
        }
    }
}