using Produtos.Api.Application.DTO;

namespace Produtos.Api.Application.Services
{
    public interface IProdutoService
    {
        Task<ProdutoReadDto> CreateAsync(ProdutoCreateDto produtoCreate, CancellationToken ct = default);
        Task<IReadOnlyList<ProdutoReadDto>> ListAsync(CancellationToken ct = default);
    }
}
