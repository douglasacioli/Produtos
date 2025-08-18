using Produtos.Api.Domain.Entities;

namespace Produtos.Api.Infrastructure.Repositories
{
    public interface IProdutoRepositoryReader
    {
        /// <summary>
        /// Retorna coleção SOMENTE LEITURA e NUNCA null (lista vazia quando não houver itens).
        /// Deve propagar cancelamento (OperationCanceledException).
        /// </summary>
        Task<IReadOnlyList<Produto>> ListProdutoAsync(CancellationToken ct = default);
    }
}
