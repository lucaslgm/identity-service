using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Data
{
    public class IdentityDbContext(DbContextOptions<DbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.Username).IsUnique();
                entity.HasIndex(e => e.IsActive);
                entity.Property(e => e.CreatedAt).ValueGeneratedOnAdd().HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.UpdatedAt).ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("GETDATE()");
            });

            // atribui o valor diretamente ao campo _atributo durante a carga inicial, ignorando o setter da entidade
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var clrType = entityType.ClrType;
                modelBuilder.Entity(clrType).UsePropertyAccessMode(PropertyAccessMode.PreferFieldDuringConstruction);
            }
        }
    }
}