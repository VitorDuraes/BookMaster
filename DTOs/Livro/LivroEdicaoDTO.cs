using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciamentoDeLivros.Models;

namespace GerenciamentoDeLivros.DTOs.Livro
{
    public class LivroEdicaoDTO
    {
        public string Titulo { get; set; }
        public int AutorId { get; set; }
    }
}