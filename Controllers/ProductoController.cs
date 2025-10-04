using EcommerceASPNETCoreWebAPI.Data;
using EcommerceASPNETCoreWebAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceASPNETCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly ProductoData _productoData;

        public ProductoController(ProductoData productoData)
        {
            this._productoData = productoData;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            List<Producto> listaProductos = await _productoData.ListarProductosAsync();
            return StatusCode(StatusCodes.Status200OK, listaProductos);
        }
    }
}
