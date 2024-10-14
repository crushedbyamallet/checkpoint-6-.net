using CP2.Domain.Interfaces.Dtos;
using FluentValidation;

namespace CP2.Application.Dtos
{
    public class FornecedorDto : IFornecedorDto
    {
        public string Nome { get; set; } = string.Empty;
        public string CNPJ { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime CriadoEm { get; set; }

        public void Validate()
        {
            var validateResult = new FornecedorDtoValidation().Validate(this);

            if (!validateResult.IsValid)
                throw new Exception(string.Join(" e ", validateResult.Errors.Select(x => x.ErrorMessage)));
        }
    }

    internal class FornecedorDtoValidation : AbstractValidator<FornecedorDto>
    {
        public FornecedorDtoValidation()
        {
            RuleFor(x => x.Nome)
                .MinimumLength(5).WithMessage(x => $"o nome deve ter pelo menos 5 caracteres")
                .NotEmpty().WithMessage(x => $"o nome é obrigatório e não pode estar vazio");

            RuleFor(x => x.CNPJ)
                .Length(14).WithMessage(x => $"o cnpj precisa conter 14 números")
                .NotEmpty().WithMessage(x => $"o cnpj não pode ser omitido");

            RuleFor(x => x.Endereco)
                .NotEmpty().WithMessage(x => $"o endereço é obrigatório e precisa ser preenchido");

            RuleFor(x => x.Telefone)
                .NotEmpty().WithMessage(x => $"o telefone deve ser fornecido");

            RuleFor(x => x.Endereco)
                .NotEmpty().WithMessage(x => $"o campo endereço deve ser informado");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(x => $"o e-mail não pode estar vazio e é obrigatório");
        }
    }
}
