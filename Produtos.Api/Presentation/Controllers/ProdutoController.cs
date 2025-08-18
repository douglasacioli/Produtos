using Microsoft.AspNetCore.Mvc;
using Produtos.Api.Application.DTO;
using Produtos.Api.Application.Services;

namespace Produtos.Api.Presentation.Controllers
{
    [ApiController]
    [Route("produto")]
    public class ProdutoController(IProdutoService service) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProdutoCreateDto dto, CancellationToken ct)
        {
            var created = await service.CreateAsync(dto, ct);
            return CreatedAtAction(nameof(List), new { }, created);
        }

        [HttpGet]
        public async Task<IActionResult> List(CancellationToken ct)
        {
            var items = await service.ListAsync(ct);
            return Ok(items);
        }
    }
}
