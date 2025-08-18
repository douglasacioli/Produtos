using Microsoft.EntityFrameworkCore;
using Produtos.Api.Domain.Entities;

namespace Produtos.Api.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Produto> Produtos => Set<Produto>();
    }
    
}
