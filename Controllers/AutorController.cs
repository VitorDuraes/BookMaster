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
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly IAutorInterface _autorInterface;
        public AutorController(IAutorInterface autorInterface)
        {
            _autorInterface = autorInterface;
        }

        [HttpGet("ListarAutores")]
        public async Task<ActionResult<ResponseModel<List<AutorModel>>>> ListarAutores()
        {
            var autores = await _autorInterface.ListarAutores();
            return Ok(autores);
        }

        [HttpGet("BuscarAutorPorId/{idAutor}")]
        public async Task<ActionResult<ResponseModel<AutorModel>>> BuscarAutorPorId(int idAutor)
        {
            var autor = await _autorInterface.BuscarAutorPorId(idAutor);
            if (autor.Status == false)
            {
                return NotFound(autor);
            }
            return Ok(autor);
        }

        [HttpGet("BuscarAutorPorNome/{nomeAutor}")]
        public async Task<ActionResult<ResponseModel<AutorModel>>> BuscarAutorPorNome(string nomeAutor)
        {
            var autor = await _autorInterface.BuscarAutorPorNome(nomeAutor);
            if (autor.Status == false)
            {
                return NotFound(autor);
            }
            return Ok(autor);
        }
        [HttpPost("AdicionarAutor")]
        public async Task<ActionResult<ResponseModel<List<AutorModel>>>> AdicionarAutor(AutorCriacaoDTO autorCriacaoDTO)
        {
            var autores = await _autorInterface.AdicionarAutor(autorCriacaoDTO);
            return Ok(autores);
        }

        [HttpPut("AtualizarAutor")]
        public async Task<ActionResult<ResponseModel<List<AutorModel>>>> AtualizarAutor(AutorEdicaoDTO autorEdicaoDTO)
        {
            var autores = await _autorInterface.AtualizarAutor(autorEdicaoDTO);
            return Ok(autores);

        }

        [HttpDelete("DeletarAutor/{idAutor}")]
        public async Task<ActionResult<ResponseModel<List<AutorModel>>>> DeletarAutor(int idAutor)
        {
            var autores = await _autorInterface.DeletarAutor(idAutor);
            return Ok(autores);




        }
    }
}