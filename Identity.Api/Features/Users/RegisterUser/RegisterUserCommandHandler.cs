using Identity.Domain.Interfaces;
using MediatR;

namespace Identity.Api.Features.Users.RegisterUser
{
    public class RegisterUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork) : IRequestHandler<RegisterUserCommand, RegisterUserResponse>
    {
        public async Task<RegisterUserResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            // 1. Validar o comando.
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            {
                return new RegisterUserResponse(success: false, null, "Email and Password are required.");
            }
        );

            await userRepository.AddAsync(request);

            // 3. Salvar as mudanças no banco de dados.
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var result = await Task.FromResult(new RegisterUserResponse(success: true, null, null));

            return result;
        }
    }
}