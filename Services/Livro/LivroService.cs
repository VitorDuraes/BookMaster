using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookMaster.Data;
using BookMaster.DTOs.Livro;
using BookMaster.Models;
using Microsoft.EntityFrameworkCore;

namespace BookMaster.Services.Livro
{
    public class LivroService : ILivroInterface
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public LivroService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ResponseModel<List<LivroModel>>> AdicionarLivro(List<LivroCriacaoDTO> livrosCriacaoDTO)
        {
            var response = new ResponseModel<List<LivroModel>>();

            try
            {
                if (livrosCriacaoDTO == null || !livrosCriacaoDTO.Any())
                {
                    response.Status = false;
                    response.Mensagem = "Nenhum livro foi enviado para cadastro.";
                    return response;
                }
                var livros = _mapper.Map<List<LivroModel>>(livrosCriacaoDTO);

                await _context.Livros.AddRangeAsync(livros);
                await _context.SaveChangesAsync();

                response.Dados = livros;
                response.Mensagem = "Livro(s) adicionado(s) com sucesso.";
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = "Erro inesperado ao adicionar livros. Detalhes: " + ex.Message;
            }

            return response;
        }

        public async Task<ResponseModel<List<LivroModel>>> AtualizarLivro(LivroEdicaoDTO livroEdicaoDTO)
        {
            ResponseModel<List<LivroModel>> response = new ResponseModel<List<LivroModel>>();
            try
            {
                var livro = await _context.Livros.FirstOrDefaultAsync(livroBanco => livroBanco.Id == livroEdicaoDTO.AutorId);
                if (livro == null)
                {
                    response.Mensagem = "Livro não encontrado.";
                    return response;
                }
                _mapper.Map<LivroModel>(livroEdicaoDTO);
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

        public async Task<ResponseModel<List<LivroDTO>>> ListarLivros()
        {
            ResponseModel<List<LivroDTO>> response = new ResponseModel<List<LivroDTO>>();
            try
            {
                var livros = await _context.Livros.Include(l => l.Autor).ToListAsync();
                response.Dados = _mapper.Map<List<LivroDTO>>(livros);
                response.Mensagem = "Livros listados com sucesso.";

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = ex.Message;

            }
            return response;


        }

        public async Task<ResponseModel<List<LivroModel>>> PesquisarLivros(string? titulo = null, int? autorId = null)
        {
            ResponseModel<List<LivroModel>> response = new ResponseModel<List<LivroModel>>();

            try
            {
                var query = _context.Livros.AsQueryable();

                if (!string.IsNullOrEmpty(titulo))
                {
                    query = query.Where(l => l.Titulo.ToLower().Contains(titulo.ToLower()));
                }

                if (autorId.HasValue)
                {
                    query = query.Where(l => l.AutorId == autorId.Value);
                }

                var livros = await query.ToListAsync();

                if (livros == null || !livros.Any())
                {
                    response.Status = false;
                    response.Mensagem = "Nenhum livro encontrado com os critérios informados.";
                    return response;
                }

                response.Dados = livros;
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