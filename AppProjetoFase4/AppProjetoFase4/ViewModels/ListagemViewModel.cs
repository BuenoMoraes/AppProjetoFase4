using Newtonsoft.Json;
using AppProjetoFase4.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace AppProjetoFase4.ViewModels
{
    public class ListagemViewModel : BaseViewModel
    {
        const string URL_GET_LIVROS = "http://192.168.0.47:8000/api/livros";
        const string URL_GET_RESERVAS = "http://192.168.0.47:8000/api/reservas";
        const string URL_GET_USUARIOS = "http://192.168.0.47:8000/api/registros";

        public ObservableCollection<Livro> Livros { get; set; }
        public ObservableCollection<Usuario> Usuarios { get; set; }
        public ObservableCollection<Reserva> Reservas { get; set; }

        private bool aguarde;
        public bool Aguarde
        {
            get { return aguarde; }
            set
            {
                aguarde = value;
                OnPropertyChanged();
            }
        }

        public ListagemViewModel()
        {
            this.Livros = new ObservableCollection<Livro>();
            this.Usuarios = new ObservableCollection<Usuario>();
            this.Reservas = new ObservableCollection<Reserva>();
        }


        public async Task GetLivros()
        {
            Aguarde = true;

            HttpClient httpClient = new HttpClient();
            var resultado = await httpClient.GetStringAsync(URL_GET_LIVROS);

            var livrosJson = JsonConvert.DeserializeObject<LivrosJson[]>(resultado);


            this.Livros.Clear();

            foreach (var livroJson in livrosJson)
                {
                    this.Livros.Add(new Livro
                    {
                        titulo = livroJson.titulo,
                        id = livroJson.id
                    });

                    Console.WriteLine("Livro recebido");

                }

            Aguarde = false;
        }

        public async Task GetUsuarios()
        {
            Aguarde = true;

            HttpClient httpClient = new HttpClient();
            var resultado = await httpClient.GetStringAsync(URL_GET_USUARIOS);

            var usuariosJson = JsonConvert.DeserializeObject<UsuarioJson[]>(resultado);

            this.Usuarios.Clear();

            foreach (var usuarioJson in usuariosJson)
            {
                this.Usuarios.Add(new Usuario
                {
                    name = usuarioJson.name,
                    id = usuarioJson.id
                });
            }

            Aguarde = false;
        }

        public async Task GetReservas()
        {
            Aguarde = true;
            HttpClient httpClient = new HttpClient();
            var resultadoLivros = await httpClient.GetStringAsync(URL_GET_LIVROS);
            var resultadoUsuario = await httpClient.GetStringAsync(URL_GET_USUARIOS);
            var resultadoReservas= await httpClient.GetStringAsync(URL_GET_RESERVAS);

            var livrosJson = JsonConvert.DeserializeObject<LivrosJson[]>(resultadoLivros);
            var usuariosJson = JsonConvert.DeserializeObject<UsuarioJson[]>(resultadoUsuario);
            var reservasJson = JsonConvert.DeserializeObject<ReservasJson[]>(resultadoReservas);

            this.Reservas.Clear();

            foreach (var reservaJson in reservasJson)
            {
                foreach (var livroJson in livrosJson)
                {
                    foreach (var usuarioJson in usuariosJson)
                    {
                        if((reservaJson.usuario_id == usuarioJson.id) && (livroJson.id == reservaJson.livro_id))
                        {
                            Console.WriteLine("reservaID" + reservaJson.usuario_id);
                            Console.WriteLine("usuarioJsonID" + usuarioJson.id);
                            Console.WriteLine("livroJsonID" + livroJson.id);
                            Console.WriteLine("reservalivro_id" + reservaJson.livro_id);
                            this.Reservas.Add(new Reserva
                            {
                                titulo_livro = livroJson.titulo,
                                name_usuario = usuarioJson.name
                            });
                        }
                    }
                }
            }

            Aguarde = false;
        }
    }

    class LivrosJson
    {
        public string titulo { get; set; }
        public string id { get; set; }
    }

    class UsuarioJson
    {
        public string name{ get; set; }
        public string id { get; set; }
    }

    class ReservasJson
    {
        public string livro_id { get; set; }
        public string usuario_id { get; set; }
    }
}

