using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CozyComfort.API.Data;
using CozyComfort.API.Models;

namespace CozyComfort.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly CozyComfortDbContext _context;

        public OrderController(CozyComfortDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetAllOrders()
        {
            try
            {
                var orders = await _context.Orders
                    .Include(o => o.Seller)
                    .Include(o => o.OrderItems)
                        .ThenInclude(i => i.Product)
                    .ToListAsync();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving orders: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            try
            {
                var order = await _context.Orders
                    .Include(o => o.Seller)
                    .Include(o => o.OrderItems)
                        .ThenInclude(i => i.Product)
                    .FirstOrDefaultAsync(o => o.OrderId == id);
                
                if (order == null)
                {
                    return NotFound();
                }
                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving order: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(Order order)
        {
            try
            {
                order.OrderDate = DateTime.Now;
                order.Status = OrderStatus.Pending;
                
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                
                return CreatedAtAction(nameof(GetOrder), new { id = order.OrderId }, order);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating order: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, Order order)
        {
            try
            {
                if (id != order.OrderId)
                {
                    return BadRequest("Order ID mismatch");
                }

                _context.Entry(order).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating order: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            try
            {
                var order = await _context.Orders.FindAsync(id);
                if (order == null)
                {
                    return NotFound();
                }

                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting order: {ex.Message}");
            }
        }

        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<List<Order>>> GetOrdersByCustomer(int customerId)
        {
            try
            {
                var orders = await _context.Orders
                    .Include(o => o.Seller)
                    .Include(o => o.OrderItems)
                        .ThenInclude(i => i.Product)
                    .Where(o => o.SellerId == customerId)
                    .ToListAsync();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving orders by customer: {ex.Message}");
            }
        }

        [HttpGet("status/{status}")]
        public async Task<ActionResult<List<Order>>> GetOrdersByStatus(OrderStatus status)
        {
            try
            {
                var orders = await _context.Orders
                    .Include(o => o.Seller)
                    .Include(o => o.OrderItems)
                        .ThenInclude(i => i.Product)
                    .Where(o => o.Status == status)
                    .ToListAsync();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving orders by status: {ex.Message}");
            }
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateOrderStatus(int id, OrderStatus status)
        {
            try
            {
                var order = await _context.Orders.FindAsync(id);
                if (order == null)
                {
                    return NotFound();
                }

                order.Status = status;
                await _context.SaveChangesAsync();
                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating order status: {ex.Message}");
            }
        }

        [HttpPost("{id}/assign")]
        public async Task<IActionResult> AssignOrder(int id, int assignedToUserId)
        {
            try
            {
                var order = await _context.Orders.FindAsync(id);
                if (order == null)
                {
                    return NotFound();
                }

                order.DistributorId = assignedToUserId;
                await _context.SaveChangesAsync();
                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error assigning order: {ex.Message}");
            }
        }

        [HttpGet("assigned/{userId}")]
        public async Task<ActionResult<List<Order>>> GetAssignedOrders(int userId)
        {
            try
            {
                var orders = await _context.Orders
                    .Include(o => o.Seller)
                    .Include(o => o.OrderItems)
                        .ThenInclude(i => i.Product)
                    .Where(o => o.DistributorId == userId)
                    .ToListAsync();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving assigned orders: {ex.Message}");
            }
        }

        [HttpGet("{id}/total")]
        public async Task<ActionResult<decimal>> GetOrderTotal(int id)
        {
            try
            {
                var order = await _context.Orders
                    .Include(o => o.OrderItems)
                        .ThenInclude(i => i.Product)
                    .FirstOrDefaultAsync(o => o.OrderId == id);
                
                if (order == null)
                {
                    return NotFound();
                }

                decimal total = order.OrderItems.Sum(i => i.Quantity * i.UnitPrice);
                return Ok(total);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error calculating order total: {ex.Message}");
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<Order>>> SearchOrders(DateTime? startDate = null, DateTime? endDate = null, OrderStatus? status = null)
        {
            try
            {
                var query = _context.Orders
                    .Include(o => o.Seller)
                    .Include(o => o.OrderItems)
                        .ThenInclude(i => i.Product)
                    .AsQueryable();

                if (startDate.HasValue)
                {
                    query = query.Where(o => o.OrderDate >= startDate.Value);
                }

                if (endDate.HasValue)
                {
                    query = query.Where(o => o.OrderDate <= endDate.Value);
                }

                if (status.HasValue)
                {
                    query = query.Where(o => o.Status == status.Value);
                }

                var orders = await query.ToListAsync();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error searching orders: {ex.Message}");
            }
        }
    }
} 