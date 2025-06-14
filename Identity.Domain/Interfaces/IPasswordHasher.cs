namespace Identity.Domain.Interfaces
{
    public interface IPasswordHasher
    {
        string GenerateSalt();
        string HashPassword(string password, string salt, string hashVersion);
        bool VerifyHashedPassword(string hashedPassword, string providedPassword, string salt);
        bool NeedsRehash(string hashedPassword);
    }
}