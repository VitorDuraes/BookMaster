using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookMaster.DTOs.Usuarios;
using BookMaster.Models;

namespace BookMaster.Services.Usuarios
{
    public interface IUsuarioInterface
    {
        Task<Usuario> Registrar(UsuarioRegistroDTO usuarioRegistroDTO);
        Task<string> Login(UsuarioLoginDTO usuarioLoginDTO);
    }
}