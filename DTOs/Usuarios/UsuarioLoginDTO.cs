using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMaster.DTOs.Usuarios
{
    public class UsuarioLoginDTO
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}