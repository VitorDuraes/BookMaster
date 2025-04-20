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

        public string Titulo { get; set; }

        public int AutorId { get; set; }
    }
}