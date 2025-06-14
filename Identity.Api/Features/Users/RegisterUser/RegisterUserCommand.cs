using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Identity.Api.Features.Users.RegisterUser
{
    public class RegisterUserCommand(string email, string password, string fullname) : IRequest<RegisterUserResponse>
    {
        public string Email { get; set; } = email;

        public string Password { get; set; } = password;

        public string FullName { get; set; } = fullname;
    }
}