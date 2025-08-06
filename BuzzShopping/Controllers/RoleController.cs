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
    public class RoleController : BaseController
    {
       

        public RoleController(AppDbContext context) : base(context) { }
      

        // GET: Role
        public async Task<IActionResult> Index()
        {
            return View(await _context.Roles.ToListAsync());
        }

        // GET: Role/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roleEntity = await _context.Roles
                .FirstOrDefaultAsync(m => m.RoleId == id);
            if (roleEntity == null)
            {
                return NotFound();
            }

            return View(roleEntity);
        }

        // GET: Role/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Role/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoleId,Name,Description")] RoleEntity roleEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roleEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(roleEntity);
        }

        // GET: Role/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roleEntity = await _context.Roles.FindAsync(id);
            if (roleEntity == null)
            {
                return NotFound();
            }
            return View(roleEntity);
        }

        // POST: Role/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoleId,Name,Description")] RoleEntity roleEntity)
        {
            if (id != roleEntity.RoleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roleEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoleEntityExists(roleEntity.RoleId))
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
            return View(roleEntity);
        }

        // GET: Role/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roleEntity = await _context.Roles
                .FirstOrDefaultAsync(m => m.RoleId == id);
            if (roleEntity == null)
            {
                return NotFound();
            }

            return View(roleEntity);
        }

        // POST: Role/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roleEntity = await _context.Roles.FindAsync(id);
            if (roleEntity != null)
            {
                _context.Roles.Remove(roleEntity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoleEntityExists(int id)
        {
            return _context.Roles.Any(e => e.RoleId == id);
        }
    }
}
