using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Business.Entities;
using BuzzShopping.Data;

namespace BuzzShopping.Controllers
{
    public class AddressController : Controller
    {
        private readonly AppDbContext _context;

        public AddressController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Address
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Addresses.Include(a => a.User);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Address/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addressEntity = await _context.Addresses
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.AddressId == id);
            if (addressEntity == null)
            {
                return NotFound();
            }

            return View(addressEntity);
        }

        // GET: Address/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email");
            return View();
        }

        // POST: Address/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AddressId,StreetAddress,StreetAddressLine2,City,PostalCode,Region,Country,IsPrimary,UserId")] AddressEntity addressEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(addressEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", addressEntity.UserId);
            return View(addressEntity);
        }

        // GET: Address/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addressEntity = await _context.Addresses.FindAsync(id);
            if (addressEntity == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", addressEntity.UserId);
            return View(addressEntity);
        }

        // POST: Address/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AddressId,StreetAddress,StreetAddressLine2,City,PostalCode,Region,Country,IsPrimary,UserId")] AddressEntity addressEntity)
        {
            if (id != addressEntity.AddressId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(addressEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddressEntityExists(addressEntity.AddressId))
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
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", addressEntity.UserId);
            return View(addressEntity);
        }

        // GET: Address/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addressEntity = await _context.Addresses
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.AddressId == id);
            if (addressEntity == null)
            {
                return NotFound();
            }

            return View(addressEntity);
        }

        // POST: Address/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var addressEntity = await _context.Addresses.FindAsync(id);
            if (addressEntity != null)
            {
                _context.Addresses.Remove(addressEntity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AddressEntityExists(int id)
        {
            return _context.Addresses.Any(e => e.AddressId == id);
        }
    }
}
