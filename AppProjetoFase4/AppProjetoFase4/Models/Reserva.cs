using System;
using System.Collections.Generic;
using System.Text;

namespace AppProjetoFase4.Models
{
    public class Reserva
    {
        public string usuario_id { get; set; }
        public string name_usuario { get; set; }
        public string livro_id { get; set; }
        public string titulo_livro{ get; set; }

        //public string inicio { get; set; }
        public string termino { get; set; }

        DateTime inicio = DateTime.Today;
        public DateTime DataInicio
        {
            get
            {
                return inicio;
            }
            set
            {
                inicio = value;
            }
        }

        DateTime dataTermino = DateTime.Today;
        public DateTime DataTermino
        {
            get
            {
                return dataTermino;
            }
            set
            {
                dataTermino = value;
            }
        }
    }
}
