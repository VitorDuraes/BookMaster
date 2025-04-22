using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BookMaster.Services.Autor;
using BookMaster.Models;
using BookMaster.DTOs.Autor;

namespace BookMaster.Controllers
{
    [ApiController]
    [Route("api/autores")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Tags("Autores")]
    public class AutorController : ControllerBase
    {
        private readonly IAutorInterface _autorInterface;
        public AutorController(IAutorInterface autorInterface)
        {
            _autorInterface = autorInterface;
        }

        /// <summary>
        /// Lista todos os autores cadastrados.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<ResponseModel<List<AutorModel>>>> ListarAutores()
        {
            var autores = await _autorInterface.ListarAutores();
            return Ok(autores);
        }

        /// <summary>
        /// Busca um autor pelo ID.
        /// </summary>
        [HttpGet("{idAutor:int}")]
        public async Task<ActionResult<ResponseModel<AutorModel>>> BuscarAutorPorId(int idAutor)
        {
            var autor = await _autorInterface.BuscarAutorPorId(idAutor);
            if (autor.Status == false)
            {
                return NotFound(autor);
            }
            return Ok(autor);
        }

        /// <summary>
        /// Busca um autor pelo nome.
        /// </summary>
        [HttpGet("nome/{nomeAutor}")]
        public async Task<ActionResult<ResponseModel<AutorModel>>> BuscarAutorPorNome(string nomeAutor)
        {
            var autor = await _autorInterface.BuscarAutorPorNome(nomeAutor);
            if (autor.Status == false)
            {
                return NotFound(autor);
            }
            return Ok(autor);
        }

        /// <summary>
        /// Adiciona um novo autor.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AdicionarLivro([FromBody] List<AutorCriacaoDTO> autorDto)
        {
            var response = await _autorInterface.AdicionarAutor(autorDto);
            return Ok(response);
        }

        /// <summary>
        /// Atualiza um autor existente.
        /// </summary>
        [HttpPut("{idAutor:int}")]
        public async Task<ActionResult<ResponseModel<List<AutorModel>>>> AtualizarAutor(AutorEdicaoDTO autorEdicaoDTO)
        {
            var autores = await _autorInterface.AtualizarAutor(autorEdicaoDTO);
            return Ok(autores);

        }

        /// <summary>
        /// Deleta um autor pelo ID.
        /// </summary>
        [HttpDelete("{idAutor:int}")]
        public async Task<ActionResult<ResponseModel<List<AutorModel>>>> DeletarAutor(int idAutor)
        {
            var autores = await _autorInterface.DeletarAutor(idAutor);
            return Ok(autores);

        }

        /// <summary>
        /// Lista autores paginados.
        /// </summary>

        [HttpGet("paginado")]
        public async Task<IActionResult> ListarAutoresPaginado(
        int page = 1,
        int pageSize = 10,
        string orderBy = "Nome",
        string direction = "asc")
        {
            var resultado = await _autorInterface.ListarAutoresPaginado(page, pageSize, orderBy, direction);
            return Ok(resultado);
        }
    }
}