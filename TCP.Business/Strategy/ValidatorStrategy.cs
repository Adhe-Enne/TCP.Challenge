using Core.Framework;
using FluentValidation;
using TCP.Business.Interfaces;
using TCP.Model.Constants;

namespace TCP.Business.Strategy
{
    // Para nada es un Strategy, pero dejamos abierta la opcion de implementarse como tal considerando escalabilidad.
    public class ValidatorStrategy<T> : IValidatorStrategy<T> where T : class, Core.Abstractions.IEntity
    {
        private readonly IValidator<T> _validator;
        public ValidatorStrategy(IValidator<T> validator)
        {
            _validator = validator;
        }

        public void ValidateFields(T entity)
        {
            FluentValidation.Results.ValidationResult validationResult = _validator.Validate(entity);

            if (validationResult.IsValid)
                return;

            string message = string.Join("\n", validationResult.Errors.Select(x => x.ErrorMessage));

            throw new TcpException($"{Messages.ENTITY_ERROR_VALIDATE}: {message}");
        }
    }
}