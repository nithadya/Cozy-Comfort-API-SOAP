using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CozyComfort.API.Data;
using CozyComfort.API.Models;

namespace CozyComfort.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly CozyComfortDbContext _context;

        public InventoryController(CozyComfortDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Inventory>>> GetAllInventories()
        {
            try
            {
                var inventories = await _context.Inventories
                    .Include(i => i.Product)
                    .Include(i => i.User)
                    .ToListAsync();
                return Ok(inventories);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving inventories: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Inventory>> GetInventory(int id)
        {
            try
            {
                var inventory = await _context.Inventories
                    .Include(i => i.Product)
                    .Include(i => i.User)
                    .FirstOrDefaultAsync(i => i.InventoryId == id);
                
                if (inventory == null)
                {
                    return NotFound();
                }
                return Ok(inventory);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving inventory: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Inventory>> CreateInventory(Inventory inventory)
        {
            try
            {
                _context.Inventories.Add(inventory);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetInventory), new { id = inventory.InventoryId }, inventory);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating inventory: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInventory(int id, Inventory inventory)
        {
            try
            {
                if (id != inventory.InventoryId)
                {
                    return BadRequest("Inventory ID mismatch");
                }

                _context.Entry(inventory).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(inventory);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating inventory: {ex.Message}");
            }
        }

        [HttpGet("product/{productId}")]
        public async Task<ActionResult<List<Inventory>>> GetInventoryByProduct(int productId)
        {
            try
            {
                var inventories = await _context.Inventories
                    .Include(i => i.Product)
                    .Include(i => i.User)
                    .Where(i => i.ProductId == productId)
                    .ToListAsync();
                return Ok(inventories);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving inventory by product: {ex.Message}");
            }
        }

        [HttpGet("owner/{ownerId}")]
        public async Task<ActionResult<List<Inventory>>> GetInventoryByOwner(int ownerId)
        {
            try
            {
                var inventories = await _context.Inventories
                    .Include(i => i.Product)
                    .Include(i => i.User)
                    .Where(i => i.UserId == ownerId)
                    .ToListAsync();
                return Ok(inventories);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving inventory by owner: {ex.Message}");
            }
        }

        [HttpGet("check-availability/{productId}/{ownerId}")]
        public async Task<ActionResult<bool>> CheckAvailability(int productId, int ownerId, int requiredQuantity)
        {
            try
            {
                var inventory = await _context.Inventories
                    .FirstOrDefaultAsync(i => i.ProductId == productId && i.UserId == ownerId);
                
                if (inventory == null)
                {
                    return Ok(false);
                }

                return Ok(inventory.StockQuantity >= requiredQuantity);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error checking availability: {ex.Message}");
            }
        }

        [HttpPost("update-stock")]
        public async Task<IActionResult> UpdateStock(int productId, int ownerId, int quantityChange)
        {
            try
            {
                var inventory = await _context.Inventories
                    .FirstOrDefaultAsync(i => i.ProductId == productId && i.UserId == ownerId);
                
                if (inventory == null)
                {
                    return NotFound("Inventory not found");
                }

                inventory.StockQuantity += quantityChange;
                inventory.LastUpdated = DateTime.Now;

                if (inventory.StockQuantity < 0)
                {
                    return BadRequest("Insufficient stock");
                }

                await _context.SaveChangesAsync();
                return Ok(inventory);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating stock: {ex.Message}");
            }
        }

        [HttpGet("low-stock")]
        public async Task<ActionResult<List<Inventory>>> GetLowStockItems(int threshold = 10)
        {
            try
            {
                var lowStockItems = await _context.Inventories
                    .Include(i => i.Product)
                    .Include(i => i.User)
                    .Where(i => i.StockQuantity <= threshold)
                    .ToListAsync();
                return Ok(lowStockItems);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving low stock items: {ex.Message}");
            }
        }

        [HttpGet("alert/{ownerId}")]
        public async Task<ActionResult<List<Inventory>>> GetStockAlerts(int ownerId, int threshold = 10)
        {
            try
            {
                var alerts = await _context.Inventories
                    .Include(i => i.Product)
                    .Include(i => i.User)
                    .Where(i => i.UserId == ownerId && i.StockQuantity <= threshold)
                    .ToListAsync();
                return Ok(alerts);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving stock alerts: {ex.Message}");
            }
        }
    }
} 