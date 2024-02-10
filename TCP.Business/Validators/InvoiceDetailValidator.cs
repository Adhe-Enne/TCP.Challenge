using FluentValidation;
using TCP.Model.Entities;

namespace TCP.Business.Validators
{
    public class InvoiceDetailValidator : AbstractValidator<InvoiceDetail>
    {
        public InvoiceDetailValidator()
        {
            RuleFor(x => x.InvoiceId).NotNull().GreaterThan(2000);
            RuleFor(x => x.ProductId).NotNull();
            RuleFor(x => x.Qty).GreaterThan(0);
            RuleFor(x => x.UnitPrice).GreaterThan(0);
            RuleFor(x => x.LineAmount).GreaterThan(0);
        }
    }
}
