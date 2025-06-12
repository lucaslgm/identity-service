namespace Identity.Domain.Entities
{
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

        [Required]
        public DateTime PasswordExpirationDate { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}