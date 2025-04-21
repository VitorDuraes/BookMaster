using AutoMapper;
using BookMaster.Data;
using BookMaster.DTOs.Autor;
using BookMaster.Models;
using Microsoft.EntityFrameworkCore;

namespace BookMaster.Services.Autor
{
    public class AutorService : IAutorInterface
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public AutorService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

        public async Task<ResponseModel<List<AutorModel>>> BuscarAutorPorNome(string nomeAutor)
        {
            ResponseModel<List<AutorModel>> response = new ResponseModel<List<AutorModel>>();
            try
            {
                var autores = await _context.Autores
                    .Where(a => (a.Nome + " " + a.Sobrenome).ToLower().Contains(nomeAutor.ToLower()))
                    .ToListAsync();

                if (autores == null || !autores.Any())
                {
                    response.Status = false;
                    response.Mensagem = "Nenhum autor encontrado.";
                    return response;
                }

                response.Dados = autores;
                response.Mensagem = "Autores encontrados com sucesso.";
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
                var autor = _mapper.Map<AutorModel>(autorCriacaoDTO);

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
                autor = _mapper.Map<AutorModel>(autorEdicaoDTO);
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


        public async Task<PaginacaoResponseModel<AutorModel>> ListarAutoresPaginado(
        int page = 1, int pageSize = 10, string orderBy = "Nome", string direction = "asc")
        {
            var query = _context.Autores.AsQueryable();

            // Ordenação dinâmica
            if (orderBy.ToLower() == "nome")
            {
                query = direction == "desc"
                    ? query.OrderByDescending(a => a.Nome)
                    : query.OrderBy(a => a.Nome);
            }

            // Paginação
            var total = await query.CountAsync();
            var autores = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginacaoResponseModel<AutorModel>
            {
                TotalItens = total,
                PaginaAtual = page,
                TamanhoPagina = pageSize,
                Dados = autores
            };
        }

        Task<ResponseModel<List<AutorModel>>> IAutorInterface.BuscarAutorPorNome(string nomeAutor)
        {
            throw new NotImplementedException();
        }
    }
}