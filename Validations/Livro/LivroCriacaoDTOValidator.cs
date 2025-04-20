using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookMaster.DTOs.Livro;
using FluentValidation;

namespace BookMaster.Validations.Livro
{
    public class LivroCriacaoDTOValidator : AbstractValidator<LivroCriacaoDTO>
    {
        public LivroCriacaoDTOValidator()
        {
            RuleFor(x => x.Titulo)
                .NotEmpty().WithMessage("O título do livro é obrigatório.")
                .MaximumLength(100).WithMessage("O título do livro deve ter 100 caracteres.");

            RuleFor(x => x.AutorId)
                .NotEmpty().WithMessage("O ID do autor é obrigatório.")
                .GreaterThan(0).WithMessage("O ID do autor deve ser maior que zero.");
        }

    }
}