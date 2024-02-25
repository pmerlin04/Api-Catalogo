using Microsoft.EntityFrameworkCore;
using APICatalogo.Models;

namespace APICatalogo.Context
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //colocando as entidades Categoria e Produtos para as tabelas "Categorias" e "Produtos" no banco de dados
        public DbSet<Categoria>? Categorias
        {get; set; }

        public DbSet<Produto>?Produtos
        { get; set; }
    }
}
