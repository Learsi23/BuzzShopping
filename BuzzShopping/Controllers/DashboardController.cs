using BuzzShopping.Data;
using Microsoft.AspNetCore.Mvc;

namespace BuzzShopping.Controllers
{
    public class DashboardController(AppDbContext context) : BaseController(context)
    {
        public IActionResult Dashboard()
        {
            return View();
        }

    }
}
