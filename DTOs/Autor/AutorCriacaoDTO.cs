using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookMaster.DTOs.Autor
{
    public class AutorCriacaoDTO
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
    }
}