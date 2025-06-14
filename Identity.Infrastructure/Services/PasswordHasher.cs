using System.Text;
using Identity.Domain.Interfaces;
using System.Security.Cryptography;
using Konscious.Security.Cryptography;
using Identity.Infrastructure.Utils;

namespace Identity.Infrastructure.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        private const string _DEFAULT_VERSION_PREFIX = "v1";
        private readonly PasswordHasherVersionParams _defaultParams;

        public PasswordHasher()
        {
            _defaultParams = PasswordHasherVersionParams.GetParams(_DEFAULT_VERSION_PREFIX); ;
        }

        // TODO: Add pepper to the password hashing process.
        public string HashPassword(string password, string salt, string hashVersion = _DEFAULT_VERSION_PREFIX)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrEmpty(salt))
                throw new ArgumentNullException(nameof(salt));

            var hash = GenerateHash(password, salt, hashVersion);
            var hexHash = Convert.ToHexString(hash);

            ClearHashBytes(hash);

            return $"{hashVersion}${hexHash}";
        }



        public bool VerifyHashedPassword(string storedPassword, string providedPassword, string salt)
        {
            if (string.IsNullOrEmpty(storedPassword))
                throw new ArgumentNullException(nameof(storedPassword));
            if (string.IsNullOrEmpty(providedPassword))
                throw new ArgumentNullException(nameof(providedPassword));
            if (string.IsNullOrEmpty(salt))
                throw new ArgumentNullException(nameof(salt));

            var parts = storedPassword.Split('$');

            if (parts.Length != 2) return false;

            var hashVersion = parts[0];
            var storedHashPasswordHex = parts[1];
            var storedHashPasswordBytes = Convert.FromHexString(storedHashPasswordHex);
            var reHashedPassword = GenerateHash(providedPassword, salt, hashVersion);
            var isSameLength = storedHashPasswordBytes.Length == reHashedPassword.Length;
            var isSameHash = CryptographicOperations.FixedTimeEquals(reHashedPassword, storedHashPasswordBytes);

            ClearHashBytes(reHashedPassword);
            ClearHashBytes(storedHashPasswordBytes);

            return isSameLength && isSameHash;
        }

        public string GenerateSalt()
        {
            var saltBytes = RandomNumberGenerator.GetBytes(_defaultParams.SaltSize);
            return Convert.ToHexString(saltBytes);
        }

        public bool NeedsRehash(string hashedPassword)
        {
            return !hashedPassword.StartsWith(_DEFAULT_VERSION_PREFIX + "$");
        }

        private byte[] GenerateHash(string password, string salt, string hashVersion)
        {
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var saltBytes = Convert.FromHexString(salt);

            var hashParams = hashVersion.Equals(_DEFAULT_VERSION_PREFIX)
            ? _defaultParams
            : PasswordHasherVersionParams.GetParams(hashVersion);

            byte[] hash;
            using (var argon2 = new Argon2id(passwordBytes)
            {
                Salt = saltBytes,
                Iterations = hashParams.Iterations,
                MemorySize = hashParams.MemorySize,
                DegreeOfParallelism = hashParams.DegreeOfParallelism
            })
            {
                hash = argon2.GetBytes(hashParams.HashSize);
            }

            ClearHashBytes(passwordBytes);
            ClearHashBytes(saltBytes);

            return hash;
        }

        private static void ClearHashBytes(byte[] hash)
        {
            Array.Clear(hash, 0, hash.Length);
        }
    }
}