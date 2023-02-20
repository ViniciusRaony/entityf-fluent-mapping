
using entityblogfluentmapping.Data;
using entityblogfluentmapping.Models;

namespace entityblogfluentmapping
{
    class Program
    {   
        private static void Main(string[] args)
        {
            using var context = new AppDataContext();
            // Adicionando User
            context.Users.Add(new User
            {
                Name = "Novo Teste",
                Email = "testando@teste.com",
                PasswordHash = "12345",
                Slug = "test-banco",
                Image = "www.google.com",
                Bio = "Fluent Mapping com EF" 
            });
            context.SaveChanges();

            // Criando Category e Post
            var user = context.Users.FirstOrDefault();
            var post = new Post 
            {
                Author = user,
                Body = "Testing",
                Category = new Category
                {
                    Name = "Devops",
                    Slug = "devops-docker"
                },
                CreateDate = System.DateTime.Now,
                // LastUpdateDate teste default value
                Slug = "teste-agora",
                Summary = "versao teste",
                // Tags = null,
                Title = "Teste Final"
            };
        }
    }
    
}
