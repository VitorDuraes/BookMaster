using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookMaster.DTOs.Autor;
using FluentValidation;

namespace BookMaster.Validations.Autor
{
    public class AutorCriacaoDTOValidator : AbstractValidator<AutorCriacaoDTO>
    {
        public AutorCriacaoDTOValidator()
        {
            RuleFor(a => a.Nome)
                .NotEmpty().WithMessage("O nome do autor é obrigatório.")
                .MaximumLength(100).WithMessage("O nome deve ter no máximo 100 caracteres.");

            RuleFor(a => a.Sobrenome)
                .NotEmpty().WithMessage("O sobrenome do autor é obrigatório.")
                .MaximumLength(100).WithMessage("O sobrenome deve ter no máximo 100 caracteres.");
        }


    }
}