using System.ComponentModel.DataAnnotations;

namespace Produtos.Api.Application.DTO
{
    public record ProdutoCreateDto(
        [Required, MinLength(2, ErrorMessage = "O nome deve ter pelo menos 2 caracteres.")] string Nome,
        [Range(0, double.MaxValue, ErrorMessage = "O valor não pode ser negativo.")] decimal Valor,
        [Required, MinLength(2, ErrorMessage = "A categoria deve ter pelo menos 2 caracteres.")] string Categoria
    );
    public record ProdutoReadDto(int Id, string Nome, decimal Valor, string Categoria);
}
