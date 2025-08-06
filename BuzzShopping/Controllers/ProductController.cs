using Business.DTOs; // Importa el DTO
using Business.Entities;
using BuzzShopping.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace BuzzShopping.Controllers
{
    public class ProductController(AppDbContext context, IWebHostEnvironment webHostEnvironment) : BaseController(context)
    {
        private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;

        // GET: Product
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Products.Include(p => p.Category);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productEntity = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (productEntity == null)
            {
                return NotFound();
            }

            return View(productEntity);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name");
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                // Lógica para guardar la imagen
                string imagePath = string.Empty;
                if (productDto.ImageFile != null && productDto.ImageFile.Length > 0)
                {
                    // Define la carpeta donde se guardarán las imágenes
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "upload");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    // Genera un nombre de archivo único
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(productDto.ImageFile.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    // Guarda el archivo en el servidor
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await productDto.ImageFile.CopyToAsync(stream);
                    }

                    // Guarda la ruta relativa para la base de datos
                    imagePath = "/upload/" + uniqueFileName;
                }
                else
                {
                    // Asigna una imagen por defecto si no se sube ninguna
                    imagePath = "/images/products/default.jpg";
                }

                // Mapea el DTO a la entidad
                var productEntity = new ProductEntity
                {
                    Code = productDto.Code,
                    Name = productDto.Name,
                    Model = productDto.Model,
                    Description = productDto.Description,
                    Price = productDto.Price,
                    SpecialOfferPrice = productDto.SpecialOfferPrice,
                    DiscountPercentage = productDto.DiscountPercentage,
                    HasSpecialOffer = productDto.HasSpecialOffer,
                    Image = imagePath, // Aquí se asigna la ruta del archivo guardado
                    ProductUrl = productDto.ProductUrl,
                    CategoryId = productDto.CategoryId,
                    Stock = productDto.Stock,
                    Active = productDto.Active,
                    IsFeatured = productDto.IsFeatured,
                    Brand = productDto.Brand,
                    CreatedDate = DateTime.UtcNow // Se genera en el servidor
                };

                _context.Add(productEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", productDto.CategoryId);
            return View(productDto); // Pasamos el DTO de vuelta a la vista
        }

        // GET: Product/Edit/5
        // ... (el resto del código de Edit, Delete, etc. no ha sido modificado)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            // Aquí deberías buscar la entidad y mapearla a un DTO para la vista
            var productEntity = await _context.Products.FindAsync(id);
            if (productEntity == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Description", productEntity.CategoryId);
            return View(productEntity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Code,Name,Model,Description,Price,SpecialOfferPrice,DiscountPercentage,HasSpecialOffer,Image,ProductUrl,CategoryId,Stock,Active,IsFeatured,Brand,CreatedDate,LastUpdatedDate")] ProductEntity productEntity)
        {
            if (id != productEntity.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductEntityExists(productEntity.ProductId))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Description", productEntity.CategoryId);
            return View(productEntity);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productEntity = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (productEntity == null)
            {
                return NotFound();
            }

            return View(productEntity);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productEntity = await _context.Products.FindAsync(id);
            if (productEntity != null)
            {
                _context.Products.Remove(productEntity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductEntityExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}