namespace Produtos.Api.Domain.Entities
{
    public class Produto
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public Decimal Valor { get; set; }
        public string? Categoria { get; set; }
    }
}
