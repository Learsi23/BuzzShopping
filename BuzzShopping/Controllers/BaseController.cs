using BuzzShopping.Data;
using BuzzShopping.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BuzzShopping.Controllers;

public class BaseController(AppDbContext context) : Controller
{
    public readonly AppDbContext _context = context;


    public override ViewResult View(string? viewName, object? model) 
    {

        ViewBag.productsNumber = GetCartCount();

        return base.View(viewName, model);
    }

    protected int GetCartCount()
    {
        int count = 0;

        string? cartJson = Request.Cookies["Cart"];

     
        if (!string.IsNullOrEmpty(cartJson))
        {

            var cart = JsonConvert.DeserializeObject<List<ProductIdAmount>>(cartJson);
            if (cart != null) 
            {
                count = cart.Count;
            }

        }


        return count;
    }
}



