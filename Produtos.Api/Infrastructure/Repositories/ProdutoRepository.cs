using Microsoft.EntityFrameworkCore;
using Produtos.Api.Domain.Entities;
using Produtos.Api.Infrastructure.Data;

namespace Produtos.Api.Infrastructure.Repositories
{
    public sealed class ProdutoRepository : IProdutoRepositoryReader, IProdutoRepositoryWriter
    {
        private readonly AppDbContext _context;
        public ProdutoRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Produto> AddProdutoAsync(Produto produto, CancellationToken ct = default)
        {
            await _context.Produtos.AddAsync(produto, ct);
            await _context.SaveChangesAsync(ct);
            return produto;
        }

        public async Task<IReadOnlyList<Produto>> ListProdutoAsync(CancellationToken ct = default)
        {
            return (await _context.Produtos
                .AsNoTracking()
                .OrderBy(p => p.Id)
                .ToListAsync(ct))
                .AsReadOnly();
        }
    }
}
