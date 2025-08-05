

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Business.Entities;
using BuzzShopping.Data;
using Business.DTOs;

namespace BuzzShopping.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Users.Include(u => u.Role);
            return View(await appDbContext.ToListAsync());
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userEntity = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userEntity == null)
            {
                return NotFound();
            }

            return View(userEntity);
        }

        // GET: User/Create
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "Name");
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserRegisterDto dto)
        {
            if (ModelState.IsValid)
            {
                var user = new UserEntity
                {
                    Name = $"{dto.FirstName} {dto.LastName}".Trim(),
                    Email = dto.Email,
                    PhoneNumber = dto.PhoneNumber!,
                    Password = dto.Password,
                    RoleId = dto.RoleId, 
                    IsActive = true,
                    CreatedDate = DateTime.UtcNow,
                    LastLoginDate = null,
                    Addresses = dto.Addresses.Select(a => new AddressEntity
                    {
                        StreetAddress = a.StreetAddress,
                        StreetAddressLine2 = a.StreetAddressLine2,
                        City = a.City,
                        PostalCode = a.PostalCode,
                        Region = a.Region,
                        Country = a.Country,
                        IsPrimary = a.IsPrimary
                    }).ToList()
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "Name", dto.RoleId);
            return View(dto);
        }
        // GET: User/Edit/5
        // Retrieves the user by ID and maps it to a DTO for editing in the view.

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userEntity = await _context.Users
                .Include(u => u.Addresses)
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (userEntity == null)
            {
                return NotFound();
            }

            var dto = new UserUpdateDto
            {
                UserId = userEntity.UserId,
                FirstName = userEntity.Name.Split(' ').FirstOrDefault() ?? "",
                LastName = string.Join(' ', userEntity.Name.Split(' ').Skip(1)),
                Email = userEntity.Email,
                PhoneNumber = userEntity.PhoneNumber,
                RoleId = userEntity.RoleId,
                Addresses = userEntity.Addresses.Select(a => new AddressDto
                {
                    StreetAddress = a.StreetAddress,
                    StreetAddressLine2 = a.StreetAddressLine2,
                    City = a.City,
                    PostalCode = a.PostalCode,
                    Region = a.Region,
                    Country = a.Country,
                    IsPrimary = a.IsPrimary
                }).ToList()
            };

            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "Name", dto.RoleId);
            return View(dto);
        }


        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "Name", dto.RoleId);
                return View(dto);
            }

            var userEntity = await _context.Users
                .Include(u => u.Addresses)
                .FirstOrDefaultAsync(u => u.UserId == dto.UserId);

            if (userEntity == null)
            {
                return NotFound();
            }

            userEntity.Name = $"{dto.FirstName} {dto.LastName}".Trim();
            userEntity.Email = dto.Email;
            userEntity.PhoneNumber = dto.PhoneNumber ?? userEntity.PhoneNumber;
            userEntity.RoleId = dto.RoleId;

            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                userEntity.Password = dto.Password;
            }

            // Reemplazar direcciones existentes (esto puede mejorarse según el caso)
            userEntity.Addresses = dto.Addresses.Select(a => new AddressEntity
            {
                StreetAddress = a.StreetAddress,
                StreetAddressLine2 = a.StreetAddressLine2,
                City = a.City,
                PostalCode = a.PostalCode,
                Region = a.Region,
                Country = a.Country,
                IsPrimary = a.IsPrimary,
                UserId = userEntity.UserId
            }).ToList();

            try
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserEntityExists(dto.UserId))
                {
                    return NotFound();
                }

                throw;
            }
        }


        // GET: User/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userEntity = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userEntity == null)
            {
                return NotFound();
            }

            return View(userEntity);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userEntity = await _context.Users.FindAsync(id);
            if (userEntity != null)
            {
                _context.Users.Remove(userEntity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserEntityExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
