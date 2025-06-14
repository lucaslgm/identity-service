using FluentValidation;
using MediatR;

namespace Identity.Api.Shared;

public class ValidationPipelineBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        // Verifica se existe algum validador para o request atual
        if (!validators.Any())
            return await next(cancellationToken); // continua para o próximo passo no pipeline (o handler)

        // Cria um contexto de validação
        var context = new ValidationContext<TRequest>(request);

        // Executa todos os validadores e coleta os erros
        var validationFailures = (await Task.WhenAll(validators.Select(validator => validator.ValidateAsync(context, cancellationToken))))
            .SelectMany(validationResult => validationResult.Errors)
            .Where(validationFailure => validationFailure is not null)
            .ToList();

        if (validationFailures.Count != 0)
            throw new ValidationException(validationFailures);

        return await next(cancellationToken);
    }
}