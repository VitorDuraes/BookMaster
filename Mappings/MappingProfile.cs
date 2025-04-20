using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookMaster.DTOs.Autor;
using BookMaster.DTOs.Livro;
using BookMaster.Models;

namespace BookMaster.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapeando DTOs de criação/edição para Model
            CreateMap<AutorCriacaoDTO, AutorModel>();
            CreateMap<AutorEdicaoDTO, AutorModel>();

            // Mapeando os Models para DTOs para retornar informações
            CreateMap<AutorModel, AutorDTO>(); // Para retornar os dados do autor
            CreateMap<LivroModel, LivroDTO>(); // Para retornar os dados do livro com o autor

            // Mapeando criação de Livro
            CreateMap<LivroCriacaoDTO, LivroModel>();
            CreateMap<LivroEdicaoDTO, LivroModel>();
        }
    }
}