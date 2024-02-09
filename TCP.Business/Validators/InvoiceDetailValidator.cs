using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCP.Model.Entities;

namespace TCP.Business.Validators
{
    public class InvoiceDetailValidator : AbstractValidator<InvoiceDetail>
    {
        public InvoiceDetailValidator()
        {
            RuleFor(x => x.ProductId).NotNull();
            RuleFor(x => x.InvoiceId).NotNull();
            RuleFor(x => x.Qty).GreaterThan(0);
            RuleFor(x => x.Price).GreaterThan(0);
        }
    }
}
