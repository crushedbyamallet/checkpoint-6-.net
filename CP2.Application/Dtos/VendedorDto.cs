using CP2.Domain.Interfaces.Dtos;
using FluentValidation;

namespace CP2.Application.Dtos
{
    public class VendedorDto : IVendedorDto
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public string Endereco { get; set; } = string.Empty;
        public DateTime DataContratacao { get; set; }
        public decimal ComissaoPercentual { get; set; }
        public decimal MetaMensal { get; set; }
        public DateTime CriadoEm { get; set; }

        public void Validate()
        {
            var validateResult = new VendedorDtoValidation().Validate(this);

            if (!validateResult.IsValid)
                throw new Exception(string.Join(" e ", validateResult.Errors.Select(x => x.ErrorMessage)));
        }
    }

    internal class VendedorDtoValidation : AbstractValidator<VendedorDto>
    {
        public VendedorDtoValidation()
        {
            RuleFor(x => x.Nome)
                .MinimumLength(5).WithMessage(x => $"o nome deve ter pelo menos 5 caracteres")
                .NotEmpty().WithMessage(x => $"o nome é obrigatório e não pode ser deixado em branco");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(x => $"o email é obrigatório e deve ser informado");

            RuleFor(x => x.Telefone)
                .NotEmpty().WithMessage(x => $"o telefone é um campo obrigatório e não pode ficar vazio");

            RuleFor(x => x.DataNascimento)
                .NotEmpty().WithMessage(x => $"a data de nascimento precisa ser preenchida corretamente");

            RuleFor(x => x.Endereco)
                .NotEmpty().WithMessage(x => $"o endereço deve ser informado e não pode estar vazio");

            RuleFor(x => x.DataContratacao)
                .NotEmpty().WithMessage(x => $"a data de contratação precisa ser fornecida");

            RuleFor(x => x.ComissaoPercentual)
                .NotEmpty().WithMessage(x => $"o campo de comissão não pode ser vazio")
                .LessThanOrEqualTo(100).WithMessage(x => $"a comissão deve ser de no máximo 100%");

            RuleFor(x => x.MetaMensal)
                .NotEmpty().WithMessage(x => $"o campo da meta mensal é obrigatório")
                .GreaterThan(0).WithMessage(x => $"a meta mensal precisa ser um valor positivo");
        }
    }
}
