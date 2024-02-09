using FluentValidation;
using TCP.Model.Entities;

namespace TCP.Business.Validators
{
    public class ClientValidator : AbstractValidator<Client>
    {
        public ClientValidator()
        {
            RuleFor(x => x.CompanyName).Length(1, 255);
            RuleFor(x => x.CUIT).Length(1,50);
            RuleFor(x => x.Adress).Length(1, 255);
        }
    }
}
