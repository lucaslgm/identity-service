using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Features.Users.RegisterUser
{
    public static class RegisterUserEndpoint
    {
        public static void MapRegisterUserEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPost("api/users/register", async ([FromBody] RegisterUserCommand command, ISender mediator) =>
            {
                try
                {
                    var userId = await mediator.Send(command);
                    // Retorna um status 201 Created com a localização do novo recurso.
                    return Results.Created($"/api/users/{userId}", new { UserId = userId });
                }
                catch (Exception ex)
                {
                    // TODO: Adicionar um tratamento de exceção mais robusto (ex: para emails duplicados)
                    return Results.BadRequest(ex.Message);
                }
            })
            .WithName("RegisterUser")
            .WithTags("Users")
            .Produces<RegisterUserResponse>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);
        }
    }
}