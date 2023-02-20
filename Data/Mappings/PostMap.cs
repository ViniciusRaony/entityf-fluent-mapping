using entityblogfluentmapping.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace entityblogfluentmapping.Data.Mappings
{
    public class PostMap : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            // Tabela
            builder.ToTable("Post");

            // Chave Primária
            builder.HasKey(x => x.Id);

            // Identity
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            // Propriedades
            builder.Property(x => x.LastUpdateDate)
                .IsRequired()
                .HasColumnName("LastUpdateDate")
                .HasColumnType("SMALLDATETIME")
                .HasDefaultValueSql("GETDATE()");
            // .HasDefaultValue(DateTime.Now.ToUniversalTime()); // UTC - Recomendado, é um structor, nunca será null

            // Índices
            builder
                .HasIndex(x => x.Slug, "IX_Post_Slug")
                .IsUnique();

            // Relacionamentos 1 para n
            builder
                .HasOne(x => x.Author)
                .WithMany(x => x.Posts)
                .HasConstraintName("FK_Post_Author")
                .OnDelete(DeleteBehavior.Cascade); 

            builder
                .HasOne(x => x.Category)
                .WithMany(x => x.Posts)
                .HasConstraintName("FK_Post_Category")
                .OnDelete(DeleteBehavior.Cascade);

            // Relacionamentos n para n
            builder
                .HasMany(x => x.Tags)
                .WithMany(x => x.Posts)
                .UsingEntity<Dictionary<string, object>>( // Entidade virtual, não existe, substitui a criação da classe.cs que surge do n para n
                    "PostTag",
                    post => post
                        .HasOne<Tag>()
                        .WithMany()
                        .HasForeignKey("PostId")
                        .HasConstraintName("FK_PostTag_PostId")
                        .OnDelete(DeleteBehavior.Cascade),
                    tag => tag
                        .HasOne<Post>()
                        .WithMany()
                        .HasForeignKey("TagId")
                        .HasConstraintName("FK_PostTag_TagId")
                        .OnDelete(DeleteBehavior.Cascade));
        }

    }

}