using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMaster.Models
{
    public class PaginacaoResponseModel<T>
    {
        public int TotalItens { get; set; }
        public int PaginaAtual { get; set; }
        public int TamanhoPagina { get; set; }
        public IEnumerable<T> Dados { get; set; }
    }
}