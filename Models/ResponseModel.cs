using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMaster.Models
{
    public class ResponseModel<T>
    {
        public T? Dados { get; set; }
        public string Mensagem { get; set; } = string.Empty;
        public bool Status { get; set; } = true;
    }
}