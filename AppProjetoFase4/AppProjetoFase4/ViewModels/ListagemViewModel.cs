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
        }


        public async Task GetLivros()
        {
            Aguarde = true;

            HttpClient httpClient = new HttpClient();
            var resultado = await httpClient.GetStringAsync(URL_GET_LIVROS);

            var livrosJson = JsonConvert.DeserializeObject<LivrosJson[]>(resultado);

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

            foreach (var usuarioJson in usuariosJson)
            {
                this.Usuarios.Add(new Usuario
                {
                    name = usuarioJson.name,
                    id = usuarioJson.id
                });

                //Console.WriteLine("Livro recebido");

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
}

