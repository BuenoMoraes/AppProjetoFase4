using AppProjetoFase4.Models;
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
    public partial class PageAddReserva : ContentPage
    {
        public Reserva Reserva { get; set; }

        DateTime inicio = DateTime.Today;
        public DateTime DataInicio
        {
            get
            {
                return Reserva.DataInicio;
            }
            set
            {
                Reserva.DataInicio = value;
            }
        }

        DateTime dataTermino = DateTime.Today;
        public DateTime DataTermino
        {
            get
            {
                return Reserva.DataTermino;
            }
            set
            {
                Reserva.DataTermino = value;
            }
        }

        public PageAddReserva()
        {
            InitializeComponent();
            this.Reserva = new Reserva();
        }

        private async void btnAddReserva_Clicked(object sender, EventArgs e)
        {
            using (var httpClient = new HttpClient())
            {
                var dataInicio = startDatePicker.Date.ToString("yyyy-MM-dd");
                var dataTermino = endDatePicker.Date.ToString("yyyy-MM-dd");

                var camposFormulario = new FormUrlEncodedContent(new[]
                {
                        new KeyValuePair<string, string>("usuario_id",enUsuario.Text),
                        new KeyValuePair<string, string>("livro_id", enLivro.Text),
                        new KeyValuePair<string, string>("inicio", dataInicio),
                        new KeyValuePair<string, string>("termino",dataTermino)
                    });
                var httpResponseMessage = await httpClient.PostAsync("http://192.168.0.47:8000/api/reservas", camposFormulario);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    await DisplayAlert("Reserva cadastrada", "Reserva cadastrado com sucesso", "Ok");
                    await Navigation.PushAsync(new ListagemMain());
                }
                else
                {
                    await DisplayAlert("Reserva Incorreta", "Verifique os dados e tente novamente", "Fechar");
                }
            }

        }

    }
}