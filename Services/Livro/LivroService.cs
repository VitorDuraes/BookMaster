using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciamentoDeLivros.Data;
using GerenciamentoDeLivros.DTOs.Livro;
using GerenciamentoDeLivros.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoDeLivros.Services.Livro
{
    public class LivroService : ILivroInterface
    {
        private readonly AppDbContext _context;

        public LivroService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseModel<List<LivroModel>>> AdicionarLivro(LivroCriacaoDTO livroCriacaoDTO)
        {
            ResponseModel<List<LivroModel>> response = new ResponseModel<List<LivroModel>>();
            try
            {
                var livro = new LivroModel()
                {
                    Titulo = livroCriacaoDTO.Titulo,
                    Autor = livroCriacaoDTO.Autor
                };
                _context.Add(livro);
                await _context.SaveChangesAsync();
                response.Dados = await _context.Livros.ToListAsync();
                response.Mensagem = "Livro adicionado com sucesso.";
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = ex.Message;
                return response;
            }
        }

        public async Task<ResponseModel<List<LivroModel>>> AtualizarLivro(LivroEdicaoDTO livroEdicaoDTO)
        {
            ResponseModel<List<LivroModel>> response = new ResponseModel<List<LivroModel>>();
            try
            {
                var livro = await _context.Livros.FirstOrDefaultAsync(livroBanco => livroBanco.Id == livroEdicaoDTO.Id);
                if (livro == null)
                {
                    response.Mensagem = "Livro não encontrado.";
                    return response;
                }
                livro.Titulo = livroEdicaoDTO.Titulo;
                livro.Autor = livroEdicaoDTO.Autor;
                _context.Update(livro);
                await _context.SaveChangesAsync();
                response.Dados = await _context.Livros.ToListAsync();
                response.Mensagem = "Livro atualizado com sucesso.";
                return response;

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = ex.Message;
                return response;
            }
        }

        public async Task<ResponseModel<LivroModel>> BuscarLivroPorId(int idLivro)
        {
            ResponseModel<LivroModel> response = new ResponseModel<LivroModel>();
            try
            {
                var livro = await _context.Livros.FindAsync(idLivro);
                if (livro == null)
                {
                    response.Status = false;
                    response.Mensagem = "Livro não encontrado.";
                    return response;
                }
                response.Dados = livro;
                response.Mensagem = "Livro encontrado com sucesso.";
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = ex.Message;
                return response;
            }
        }

        public async Task<ResponseModel<List<LivroModel>>> BuscarLivrosPorIdAutor(int idAutor)
        {
            ResponseModel<List<LivroModel>> response = new ResponseModel<List<LivroModel>>();
            try
            {
                var livro = await _context.Livros.Include(a => a.Autor).Where(livrobanco => livrobanco.Autor.Id == idAutor).ToListAsync();
                if (livro == null)
                {
                    response.Mensagem = "Livro não encontrado.";
                    return response;
                }
                response.Dados = livro;
                response.Mensagem = "Livro encontrado com sucesso.";
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = ex.Message;
                return response;
            }
        }

        public async Task<ResponseModel<List<LivroModel>>> DeletarLivro(int idLivro)
        {
            ResponseModel<List<LivroModel>> response = new ResponseModel<List<LivroModel>>();
            try
            {
                var livro = _context.Livros.FirstOrDefault(livroBanco => livroBanco.Id == idLivro);
                if (livro == null)
                {

                    response.Mensagem = "Livro não encontrado.";
                    return response;
                }
                _context.Remove(livro);
                await _context.SaveChangesAsync();
                response.Dados = await _context.Livros.ToListAsync();
                response.Mensagem = "Livro deletado com sucesso.";
                return response;


            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = ex.Message;
                return response;
            }

        }

        public async Task<ResponseModel<List<LivroModel>>> ListarLivros()
        {
            ResponseModel<List<LivroModel>> response = new ResponseModel<List<LivroModel>>();
            try
            {
                var livro = await _context.Livros.ToListAsync();
                response.Dados = livro;
                response.Mensagem = "Livros listados com sucesso.";
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