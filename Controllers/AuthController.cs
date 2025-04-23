using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookMaster.DTOs.Usuarios;
using BookMaster.Services.Usuarios;
using Microsoft.AspNetCore.Mvc;

namespace BookMaster.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Tags("Cadastro de Usuários")]
    [ApiExplorerSettings(GroupName = "v1")]

    public class AuthController : ControllerBase
    {
        private readonly IUsuarioInterface _usuarioInterface;

        public AuthController(IUsuarioInterface usuarioInterface)
        {
            _usuarioInterface = usuarioInterface;
        }

        /// <summary>
        /// Registra um novo usuário.
        ///</summary>
        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar(UsuarioRegistroDTO usuarioRegistroDTO)
        {
            var usuario = await _usuarioInterface.Registrar(usuarioRegistroDTO);
            return Ok(new
            {
                usuario.Nome,
                usuario.Email,
                usuario.Perfil,

            });
        }

        /// <summary>
        /// Realiza o login do usuário.
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login(UsuarioLoginDTO usuarioLoginDTO)
        {
            var token = await _usuarioInterface.Login(usuarioLoginDTO);
            return Ok(new { Token = token });
        }

    }
}