using BuzzShopping.Data;
using Microsoft.AspNetCore.Mvc;

namespace BuzzShopping.Controllers
{
    public class BaseController(AppDbContext context) : Controller
    {
        public readonly AppDbContext _context = context;
    }
}
