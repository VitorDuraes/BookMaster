using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciamentoDeLivros.Data;
using GerenciamentoDeLivros.DTOs.Autor;
using GerenciamentoDeLivros.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoDeLivros.Services.Autor
{
    public class AutorService : IAutorInterface
    {
        private readonly AppDbContext _context;

        public AutorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<List<AutorModel>>> ListarAutores()
        {
            ResponseModel<List<AutorModel>> response = new ResponseModel<List<AutorModel>>();
            try
            {
                var autores = await _context.Autores.ToListAsync();
                response.Dados = autores;
                response.Mensagem = "Autores listados com sucesso.";
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = ex.Message;
                return response;
            }

        }

        public async Task<ResponseModel<AutorModel>> BuscarAutorPorId(int idAutor)
        {
            ResponseModel<AutorModel> response = new ResponseModel<AutorModel>();
            try
            {
                var autor = await _context.Autores.FindAsync(idAutor);
                if (autor == null)
                {
                    response.Status = false;
                    response.Mensagem = "Autor não encontrado.";
                    return response;
                }
                response.Dados = autor;
                response.Mensagem = "Autor encontrado com sucesso.";
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = ex.Message;
                return response;
            }
        }

        public async Task<ResponseModel<AutorModel>> BuscarAutorPorIdLivro(int idLivro)
        {

            ResponseModel<AutorModel> response = new ResponseModel<AutorModel>();
            try
            {
                var autor = await _context.Livros.Include(l => l.Autor).FirstOrDefaultAsync(l => l.Id == idLivro);
                if (autor == null)
                {
                    response.Status = false;
                    response.Mensagem = "Autor não encontrado.";
                    return response;
                }
                response.Dados = autor.Autor;
                response.Mensagem = "Autor encontrado com sucesso.";
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = ex.Message;
                return response;
            }
        }

        public async Task<ResponseModel<List<AutorModel>>> AdicionarAutor(AutorCriacaoDTO autorCriacaoDTO)
        {
            ResponseModel<List<AutorModel>> response = new ResponseModel<List<AutorModel>>();
            try
            {
                var autor = new AutorModel()
                {
                    Nome = autorCriacaoDTO.Nome,
                    Sobrenome = autorCriacaoDTO.Sobrenome
                };
                _context.Add(autor);
                await _context.SaveChangesAsync();
                response.Dados = await _context.Autores.ToListAsync();
                response.Mensagem = "Autor adicionado com sucesso.";
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = ex.Message;
                return response;
            }
        }

        public async Task<ResponseModel<List<AutorModel>>> AtualizarAutor(AutorEdicaoDTO autorEdicaoDTO)
        {
            ResponseModel<List<AutorModel>> response = new ResponseModel<List<AutorModel>>();
            try
            {
                var autor = await _context.Autores.FirstOrDefaultAsync(autorBanco => autorBanco.Id == autorEdicaoDTO.Id);
                if (autor == null)
                {
                    response.Mensagem = "Autor não encontrado.";
                    return response;
                }
                autor.Nome = autorEdicaoDTO.Nome;
                autor.Sobrenome = autorEdicaoDTO.Sobrenome;
                _context.Update(autor);
                await _context.SaveChangesAsync();
                response.Dados = await _context.Autores.ToListAsync();
                response.Mensagem = "Autor atualizado com sucesso.";
                return response;

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = ex.Message;
                return response;
            }
        }

        public async Task<ResponseModel<List<AutorModel>>> DeletarAutor(int idAutor)
        {
            ResponseModel<List<AutorModel>> response = new ResponseModel<List<AutorModel>>();
            try
            {
                var autor = _context.Autores.FirstOrDefault(autorBanco => autorBanco.Id == idAutor);
                if (autor == null)
                {

                    response.Mensagem = "Autor não encontrado.";
                    return response;
                }
                _context.Remove(autor);
                await _context.SaveChangesAsync();
                response.Dados = await _context.Autores.ToListAsync();
                response.Mensagem = "Autor deletado com sucesso.";
                return response;


            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = ex.Message;
                return response;
            }

        }
    }
}