namespace Identity.Api.Features.Users.RegisterUser
{
    public class RegisterUserResponse(bool success, int? userId = null, string errorMessage = null)
    {
        public bool Success { get; set; } = success;
        public int? UserId { get; set; } = userId;
        public string ErrorMessage { get; set; } = errorMessage;
    }
}