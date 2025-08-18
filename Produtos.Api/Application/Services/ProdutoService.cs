using Produtos.Api.Application.DTO;
using Produtos.Api.Domain.Entities;
using Produtos.Api.Infrastructure.Repositories;

namespace Produtos.Api.Application.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepositoryReader _produtoRepositoryReader;
        private readonly IProdutoRepositoryWriter _produtoRepositoryWriter;

        public ProdutoService(IProdutoRepositoryWriter writer, IProdutoRepositoryReader reader)
        {
            _produtoRepositoryWriter = writer;
            _produtoRepositoryReader = reader;
        }
        public async Task<ProdutoReadDto> CreateAsync(ProdutoCreateDto produtoDTO, CancellationToken ct = default)
        {
            var produto = new Produto
            {
                Nome = produtoDTO.Nome,
                Valor = produtoDTO.Valor,
                Categoria = produtoDTO.Categoria
            };

            var created = await _produtoRepositoryWriter.AddProdutoAsync(produto, ct);

            return new ProdutoReadDto(created.Id, created.Nome, created.Valor, created.Categoria);
        }

        public async Task<IReadOnlyList<ProdutoReadDto>> ListAsync(CancellationToken ct = default)
        {
            var produtos = await _produtoRepositoryReader.ListProdutoAsync(ct);
            return produtos.Select(p => new ProdutoReadDto(p.Id, p.Nome, p.Valor, p.Categoria)).ToList();
        }
    }
}
