using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CozyComfort.API.Data;
using CozyComfort.API.Models;

namespace CozyComfort.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly CozyComfortDbContext _context;

        public ProductController(CozyComfortDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            try
            {
                var products = await _context.Products.ToListAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving products: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving product: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            try
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId }, product);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating product: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            try
            {
                if (id != product.ProductId)
                {
                    return BadRequest("Product ID mismatch");
                }

                _context.Entry(product).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating product: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    return NotFound();
                }

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting product: {ex.Message}");
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<Product>>> SearchProducts(string? name = null, string? category = null, decimal? minPrice = null, decimal? maxPrice = null)
        {
            try
            {
                var query = _context.Products.AsQueryable();

                if (!string.IsNullOrEmpty(name))
                {
                    query = query.Where(p => p.ProductName.Contains(name));
                }

                if (!string.IsNullOrEmpty(category))
                {
                    query = query.Where(p => p.Material.Contains(category));
                }

                if (minPrice.HasValue)
                {
                    query = query.Where(p => p.Price >= minPrice.Value);
                }

                if (maxPrice.HasValue)
                {
                    query = query.Where(p => p.Price <= maxPrice.Value);
                }

                var products = await query.ToListAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error searching products: {ex.Message}");
            }
        }

        [HttpGet("category/{category}")]
        public async Task<ActionResult<List<Product>>> GetProductsByCategory(string category)
        {
            try
            {
                var products = await _context.Products
                    .Where(p => p.Material == category)
                    .ToListAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving products by category: {ex.Message}");
            }
        }

        [HttpGet("categories")]
        public async Task<ActionResult<List<string>>> GetCategories()
        {
            try
            {
                var categories = await _context.Products
                    .Select(p => p.Material)
                    .Distinct()
                    .ToListAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving categories: {ex.Message}");
            }
        }
    }
} 