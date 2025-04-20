using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookMaster.DTOs.Autor
{
    public class AutorCriacaoDTO
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O sobrenome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O sobrenome deve ter no máximo 100 caracteres.")]
        public string Sobrenome { get; set; }
    }
}