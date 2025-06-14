using FluentValidation;

namespace Identity.Api.Middlewares;

public class GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // Tenta executar o próximo middleware no pipeline.
            // Se uma exceção ocorrer, ela será capturada pelo bloco catch.
            await next(context);
        }
        catch (Exception ex)
        {
            // Se ocorrer uma exceção, processa e formata a resposta.
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        // Loga a exceção para monitoramento
        logger.LogError(exception, "Uma exceção não tratada ocorreu: {Message}", exception.Message);

        // Verifica se é uma exceção de validação do FluentValidation
        if (exception is ValidationException validationException)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            var errors = validationException.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

            await context.Response.WriteAsJsonAsync(new
            {
                Title = "Erro de Validação",
                Status = StatusCodes.Status400BadRequest,
                Errors = errors
            });
        }
        else // Para todas as outras exceções
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new
            {
                Title = "Erro Interno do Servidor",
                Status = StatusCodes.Status500InternalServerError,
                Detail = "Ocorreu um erro inesperado. Por favor, tente novamente mais tarde."
            });
        }
    }
}