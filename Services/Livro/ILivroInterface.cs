using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciamentoDeLivros.DTOs.Autor;
using GerenciamentoDeLivros.DTOs.Livro;
using GerenciamentoDeLivros.Models;

namespace GerenciamentoDeLivros.Services.Livro
{
    public interface ILivroInterface
    {
        Task<ResponseModel<List<LivroDTO>>> ListarLivros();
        Task<ResponseModel<List<LivroModel>>> BuscarLivrosPorIdAutor(int idAutor);
        Task<ResponseModel<LivroDTO>> AdicionarLivro(LivroCriacaoDTO livroDTO);
        Task<ResponseModel<List<LivroModel>>> AtualizarLivro(LivroEdicaoDTO livroEdicaoDTO);
        Task<ResponseModel<List<LivroModel>>> DeletarLivro(int idLivro);


    }
}