namespace Identity.Domain.Entities
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(256)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "varchar(256)")]
        public string Email { get; set; }

        [Required]
        [Column(TypeName = "varchar(32)")]
        public string Username { get; set; }

        [Required]
        [Column(TypeName = "varchar(128)")]
        public string Salt { get; set; }

        [Required]
        [Column(TypeName = "varchar(256)")]
        public string HashedPassword { get; set; }

        [Required]
        public bool IsActive { get; set; }

        // [Required]
        // public DateTime PasswordExpirationDate { get; set; }

        [Column(TypeName = "smallint")]
        [DefaultValue(0)]
        public int FailedLoginAttempts { get; set; }

        public DateTime? LockoutEndDate { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public static User Create(string name, string email, string username, string plainPassword)
        {
            var salt = Guid.NewGuid().ToString("N")[..32];
            var hashedPassword = HashPassword(plainPassword, salt);
            var now = DateTime.UtcNow;

            return new User
            {
                Name = name,
                Email = email,
                Username = GenerateUsername(username),
                Salt = salt,
                HashedPassword = hashedPassword,
                IsActive = true,
                PasswordExpirationDate = now.AddMonths(6),
                CreatedAt = now,
                UpdatedAt = now
            };
        }

        public static string GenerateUsername(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null or empty.", nameof(name));

            var username = new string([.. name
                .ToLowerInvariant()
                .Trim()
                .Where(char.IsLetterOrDigit)]);

            if (username.Length > 6)
                username = username[..6];

            var random = new Random();
            var suffix = random.Next(1000, 10000);
            return $"{username}{suffix}";
        }
    }
}