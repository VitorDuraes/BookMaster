using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BookMaster.Models;

namespace BookMaster.DTOs.Livro
{
    public class LivroCriacaoDTO
    {
        [Required(ErrorMessage = "O título é obrigatório.")]
        [StringLength(200, ErrorMessage = "O título deve ter no máximo 200 caracteres.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O ID do autor é obrigatório.")]
        public int AutorId { get; set; }
    }
}