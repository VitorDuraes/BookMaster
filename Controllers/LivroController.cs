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

    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly ILivroInterface _livroInterface;
        public LivroController(ILivroInterface livroInterface)
        {
            _livroInterface = livroInterface;
        }

        [HttpGet("ListarLivros")]
        public async Task<ActionResult<ResponseModel<List<LivroModel>>>> ListarLivros()
        {
            var livros = await _livroInterface.ListarLivros();
            return Ok(livros);
        }

        [HttpGet("PesquisarLivros")]
        public async Task<ActionResult<ResponseModel<List<LivroModel>>>> PesquisarLivros(string? titulo, int? autorId)
        {
            var resultado = await _livroInterface.PesquisarLivros(titulo, autorId);
            if (!resultado.Status)
                return NotFound(resultado.Mensagem);

            return Ok(resultado);

        }
        [HttpGet("BuscarLivrosPorIdAutor/{idAutor}")]
        public async Task<ActionResult<ResponseModel<LivroModel>>> BuscarLivrosPorIdAutor(int idAutor)
        {
            var livro = await _livroInterface.BuscarLivrosPorIdAutor(idAutor);
            if (livro.Status == false)
            {
                return NotFound(livro);
            }
            return Ok(livro);
        }

        [HttpPost("AdicionarLivro")]
        public async Task<ActionResult<ResponseModel<List<LivroModel>>>> AdicionarLivro(LivroCriacaoDTO livroCriacaoDTO)
        {
            var livros = await _livroInterface.AdicionarLivro(livroCriacaoDTO);
            return Ok(livros);
        }

        [HttpPut("AtualizarLivro")]
        public async Task<ActionResult<ResponseModel<List<LivroModel>>>> AtualizarLivro(LivroEdicaoDTO livroEdicaoDTO)
        {
            var livros = await _livroInterface.AtualizarLivro(livroEdicaoDTO);
            return Ok(livros);

        }

        [HttpDelete("DeletarLivro/{idLivro}")]
        public async Task<ActionResult<ResponseModel<List<LivroModel>>>> DeletarLivro(int idLivro)
        {
            var livros = await _livroInterface.DeletarLivro(idLivro);
            return Ok(livros);

        }


    }
}