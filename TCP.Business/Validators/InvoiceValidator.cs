using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCP.Model.Entities;

namespace TCP.Business.Validators
{
    public class InvoiceValidator : AbstractValidator<Invoice>
    {
        public InvoiceValidator()
        {
            //RuleFor(x => x.Id).GreaterThan(2000).WithMessage("");
            RuleFor(x => x.ClientId)
                .NotNull().WithMessage("¡Numero de Cliente puede ser vacio!")
                .GreaterThan(1000).WithMessage("¡Numero de Cliente debe ser mayor a 1000!");

            RuleFor(x => x.TotalAmount).GreaterThan(0);
        }
    }
}
