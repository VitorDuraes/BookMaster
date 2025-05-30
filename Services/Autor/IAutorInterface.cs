using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookMaster.DTOs.Autor;
using BookMaster.Models;

namespace BookMaster.Services.Autor
{
    public interface IAutorInterface
    {
        Task<ResponseModel<List<AutorModel>>> ListarAutores();
        Task<ResponseModel<AutorModel>> BuscarAutorPorId(int idAutor);
        Task<ResponseModel<List<AutorModel>>> AdicionarAutor(List<AutorCriacaoDTO> autorCriacaoDTO);
        Task<ResponseModel<List<AutorModel>>> AtualizarAutor(AutorEdicaoDTO autorEdicaoDTO);
        Task<ResponseModel<List<AutorModel>>> DeletarAutor(int idAutor);
        Task<ResponseModel<List<AutorModel>>> BuscarAutorPorNome(string nomeAutor);
        Task<PaginacaoResponseModel<AutorModel>> ListarAutoresPaginado(int page, int pageSize, string orderBy, string direction);



    }
}