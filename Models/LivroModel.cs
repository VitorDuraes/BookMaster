using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMaster.Models
{
    public class LivroModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }

        // Adicione essa propriedade:
        public int AutorId { get; set; } // chave estrangeira

        // Navegação
        public AutorModel Autor { get; set; }
    }
}