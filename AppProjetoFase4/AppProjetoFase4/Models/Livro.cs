using System;
using System.Collections.Generic;
using System.Text;

namespace AppProjetoFase4.Models
{
    public class Livro
    {
        public string id { get; set; }
        public string autor_id { get; set; }
        public string status_id { get; set; }
        public string titulo { get; set; }
        public string anoPublicacao { get; set; }

        public string image { get; set; }

    }
}
