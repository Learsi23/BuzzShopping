using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Business.Entities;
using BuzzShopping.Data;

namespace BuzzShopping.Controllers
{
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Order
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Orders.Include(o => o.BillingAddress).Include(o => o.ShippingAddress).Include(o => o.User);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Order/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderEntity = await _context.Orders
                .Include(o => o.BillingAddress)
                .Include(o => o.ShippingAddress)
                .Include(o => o.User)
                .Include(o => o.OrderDetails)
                   .ThenInclude(od => od.Product)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (orderEntity == null)
            {
                return NotFound();
            }

            return View(orderEntity);
        }

        // GET: Order/Create
        public IActionResult Create()
        {
            ViewData["BillingAddressId"] = new SelectList(_context.Addresses, "AddressId", "City");
            ViewData["ShippingAddressId"] = new SelectList(_context.Addresses, "AddressId", "City");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email");
            return View();
        }

        // POST: Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,OrderNumber,UserId,CustomerName,CustomerEmail,CustomerPhone,CreatedDate,PaymentDate,ShippedDate,Status,ShippingAddressId,BillingAddressId,Subtotal,ShippingCost,DiscountAmount,Total")] OrderEntity orderEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BillingAddressId"] = new SelectList(_context.Addresses, "AddressId", "City", orderEntity.BillingAddressId);
            ViewData["ShippingAddressId"] = new SelectList(_context.Addresses, "AddressId", "City", orderEntity.ShippingAddressId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", orderEntity.UserId);
            return View(orderEntity);
        }

        // GET: Order/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderEntity = await _context.Orders.FindAsync(id);
            if (orderEntity == null)
            {
                return NotFound();
            }
            ViewData["BillingAddressId"] = new SelectList(_context.Addresses, "AddressId", "City", orderEntity.BillingAddressId);
            ViewData["ShippingAddressId"] = new SelectList(_context.Addresses, "AddressId", "City", orderEntity.ShippingAddressId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", orderEntity.UserId);
            return View(orderEntity);
        }

        // POST: Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,OrderNumber,UserId,CustomerName,CustomerEmail,CustomerPhone,CreatedDate,PaymentDate,ShippedDate,Status,ShippingAddressId,BillingAddressId,Subtotal,ShippingCost,DiscountAmount,Total")] OrderEntity orderEntity)
        {
            if (id != orderEntity.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderEntityExists(orderEntity.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BillingAddressId"] = new SelectList(_context.Addresses, "AddressId", "City", orderEntity.BillingAddressId);
            ViewData["ShippingAddressId"] = new SelectList(_context.Addresses, "AddressId", "City", orderEntity.ShippingAddressId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", orderEntity.UserId);
            return View(orderEntity);
        }

        // GET: Order/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderEntity = await _context.Orders
                .Include(o => o.BillingAddress)
                .Include(o => o.ShippingAddress)
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (orderEntity == null)
            {
                return NotFound();
            }

            return View(orderEntity);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderEntity = await _context.Orders.FindAsync(id);
            if (orderEntity != null)
            {
                _context.Orders.Remove(orderEntity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderEntityExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}
