using Produtos.Api.Domain.Entities;

namespace Produtos.Api.Infrastructure.Repositories
{
    public interface IProdutoRepositoryWriter
    {
        /// <summary>
        /// Persiste e retorna o Produto com Id gerado (> 0).
        /// Nunca retorna null.
        /// Deve propagar cancelamento (OperationCanceledException).
        /// </summary>
        Task<Produto> AddProdutoAsync(Produto product, CancellationToken ct = default);
    }
}
