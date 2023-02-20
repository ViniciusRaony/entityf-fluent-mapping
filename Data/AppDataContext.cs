using entityblogfluentmapping.Models;
using entityblogfluentmapping.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace entityblogfluentmapping.Data
{
    // AppDataContext se torna um banco em memória, procura-se executar leitura -> modela dados -> aplica regra de negócio -> salva no banco.
    public class AppDataContext : DbContext 
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Tag> Tags { get; set; }

        // Conectar com o banco, método , aplicando sobrescrita de método (ovverrider)
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Server=localhost,1433;Database=Blog;User Id=sa;Password=Test123;TrustServerCertificate=True");
        
        // Informando ao DataContext que temos arquivos de mapeamento
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryMap());   
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new PostMap());
        }
    }
    
}