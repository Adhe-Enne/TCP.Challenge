using FluentValidation;
using TCP.Model.Entities;

namespace TCP.Business.Validators
{
    public class ClientValidator : AbstractValidator<Client>
    {
        public ClientValidator()
        {
            RuleFor(x => x.CompanyName).NotNull().Length(1, 255);
            RuleFor(x => x.CUIT).NotNull().Length(1,50);
            RuleFor(x => x.Adress).NotNull().Length(1, 255);
            RuleFor(x => x.Email).NotNull().Length(1, 80).EmailAddress();
            RuleFor(x => x.Phone).NotNull().Length(1, 20);
        }
    }
}