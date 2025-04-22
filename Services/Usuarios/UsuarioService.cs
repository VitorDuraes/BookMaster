using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BookMaster.Data;
using BookMaster.DTOs.Usuarios;
using BookMaster.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BookMaster.Services.Usuarios
{
    public class UsuarioService : IUsuarioInterface
    {
        private AppDbContext _context;
        private readonly IConfiguration _config;

        public UsuarioService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _config = configuration;
        }

        public async Task<Usuario> Registrar(UsuarioRegistroDTO usuarioRegistroDTO)
        {
            var usuarioExistente = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == usuarioRegistroDTO.Email);
            if (usuarioExistente != null)
            {
                throw new Exception("Email já cadastrado.");
            }
            var usuario = new Usuario
            {
                Nome = usuarioRegistroDTO.Nome,
                Email = usuarioRegistroDTO.Email,
                SenhaHask = BCrypt.Net.BCrypt.HashPassword(usuarioRegistroDTO.Senha),
                Perfil = usuarioRegistroDTO.Perfil
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }

        public async Task<string> Login(UsuarioLoginDTO usuarioLoginDTO)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == usuarioLoginDTO.Email);
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(usuarioLoginDTO.Senha, usuario.SenhaHask))
            {
                throw new Exception("Credenciais inválidas.");
            }

            return GerarTokenJWT(usuario);
        }

        private string GerarTokenJWT(Usuario usuario)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, usuario.Email),
            new Claim(ClaimTypes.Role, usuario.Perfil)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}