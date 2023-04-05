using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VerytelPizzaShop.Entity;
using VerytelPizzaShop.Models;

namespace VerytelPizzaShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly OrdersDbContext _dbContext;

        public OrdersController(OrdersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _dbContext.Orders.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _dbContext.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        [HttpPost]
        [Route("CreateOrder")]
        public async Task<ActionResult<Order>> CreateOrderAsync(Order order)
        {
            if (!Request.ContentType.Equals("application/json", StringComparison.OrdinalIgnoreCase))
            {
                return new ObjectResult(new
                {
                    error = "The request must have a content type of 'application/json'"
                })
                {
                    StatusCode = 415
                };
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            _dbContext.Entry(order).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        /*
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _dbContext.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
        */
        private bool OrderExists(int id)
        {
            return _dbContext.Orders.Any(e => e.Id == id);
        }
            
        [HttpOptions]
        public IActionResult Preflight()
        {
            Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type");
            return Ok();
        }
    }
}
