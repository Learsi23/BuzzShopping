using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Business.Entities;
using BuzzShopping.Data;

namespace BuzzShopping.Controllers
{
    public class OrderDetailController : Controller
    {
        private readonly AppDbContext _context;

        public OrderDetailController(AppDbContext context)
        {
            _context = context;
        }

        // GET: OrderDetail
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.OrderDetails.Include(o => o.Order).Include(o => o.Product);
            return View(await appDbContext.ToListAsync());
        }

        // GET: OrderDetail/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetailEntity = await _context.OrderDetails
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.OrderDetailId == id);
            if (orderDetailEntity == null)
            {
                return NotFound();
            }

            return View(orderDetailEntity);
        }

        // GET: OrderDetail/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "CustomerEmail");
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Brand");
            return View();
        }

        // POST: OrderDetail/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderDetailId,ProductId,OrderId,Amount,UnitPriceAtPurchase,DiscountApplied,LineTotal,CreatedDate,Status")] OrderDetailEntity orderDetailEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderDetailEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "CustomerEmail", orderDetailEntity.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Brand", orderDetailEntity.ProductId);
            return View(orderDetailEntity);
        }

        // GET: OrderDetail/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetailEntity = await _context.OrderDetails.FindAsync(id);
            if (orderDetailEntity == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "CustomerEmail", orderDetailEntity.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Brand", orderDetailEntity.ProductId);
            return View(orderDetailEntity);
        }

        // POST: OrderDetail/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderDetailId,ProductId,OrderId,Amount,UnitPriceAtPurchase,DiscountApplied,LineTotal,CreatedDate,Status")] OrderDetailEntity orderDetailEntity)
        {
            if (id != orderDetailEntity.OrderDetailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderDetailEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderDetailEntityExists(orderDetailEntity.OrderDetailId))
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
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "CustomerEmail", orderDetailEntity.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Brand", orderDetailEntity.ProductId);
            return View(orderDetailEntity);
        }

        // GET: OrderDetail/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetailEntity = await _context.OrderDetails
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.OrderDetailId == id);
            if (orderDetailEntity == null)
            {
                return NotFound();
            }

            return View(orderDetailEntity);
        }

        // POST: OrderDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderDetailEntity = await _context.OrderDetails.FindAsync(id);
            if (orderDetailEntity != null)
            {
                _context.OrderDetails.Remove(orderDetailEntity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderDetailEntityExists(int id)
        {
            return _context.OrderDetails.Any(e => e.OrderDetailId == id);
        }
    }
}
