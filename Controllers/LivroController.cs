using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookMaster.DTOs.Livro;
using BookMaster.Models;
using BookMaster.Services.Livro;
using Microsoft.AspNetCore.Mvc;

namespace BookMaster.Controllers
{

    [ApiController]
    [Route("api/livros")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Tags("Livros")]
    public class LivroController : ControllerBase
    {
        private readonly ILivroInterface _livroInterface;
        public LivroController(ILivroInterface livroInterface)
        {
            _livroInterface = livroInterface;
        }

        /// <summary>
        /// Lista todos os livros cadastrados.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<ResponseModel<List<LivroModel>>>> ListarLivros()
        {
            var livros = await _livroInterface.ListarLivros();
            return Ok(livros);
        }

        /// <summary>
        /// Busca um livro pelo Nome.
        ///</summary>
        [HttpGet("pesquisa")]
        public async Task<ActionResult<ResponseModel<List<LivroModel>>>> PesquisarLivros(string? titulo, int? autorId)
        {
            var resultado = await _livroInterface.PesquisarLivros(titulo, autorId);
            if (!resultado.Status)
                return NotFound(resultado.Mensagem);

            return Ok(resultado);

        }

        /// <summary>
        /// Busca um livro pelo ID do Autor.
        /// </summary>
        [HttpGet("autor/{idAutor}")]
        public async Task<ActionResult<ResponseModel<LivroModel>>> BuscarLivrosPorIdAutor(int idAutor)
        {
            var livro = await _livroInterface.BuscarLivrosPorIdAutor(idAutor);
            if (livro.Status == false)
            {
                return NotFound(livro);
            }
            return Ok(livro);
        }

        /// <summary>
        /// Adiciona um novo livro.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AdicionarLivro([FromBody] List<LivroCriacaoDTO> livrosDto)
        {
            var response = await _livroInterface.AdicionarLivro(livrosDto);
            return Ok(response);
        }

        /// <summary>
        /// Atualiza um livro existente.
        /// </summary>

        [HttpPut("{idLivro}")]
        public async Task<ActionResult<ResponseModel<List<LivroModel>>>> AtualizarLivro(LivroEdicaoDTO livroEdicaoDTO)
        {
            var livros = await _livroInterface.AtualizarLivro(livroEdicaoDTO);
            return Ok(livros);

        }
        /// <summary>
        /// Deleta um livro existente.
        /// </summary>

        [HttpDelete("{idLivro}")]
        public async Task<ActionResult<ResponseModel<List<LivroModel>>>> DeletarLivro(int idLivro)
        {
            var livros = await _livroInterface.DeletarLivro(idLivro);
            return Ok(livros);

        }


    }
}