using DominandoEF.MultiTenant.Data;
using DominandoEF.MultiTenant.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace DominandoEF.MultiTenant.Controllers
{
    [ApiController]
    [Route("{tenant}/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;

        public ProductController(ILogger<PersonController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Product> Get([FromServices] ApplicationContext db)
        {
            var products = db.Products.ToArray();

            return products;
        }
    }
}
