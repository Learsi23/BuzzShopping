using Business.ViewModels;
using BuzzShopping.Data;
using BuzzShopping.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data.Common;
using System.Diagnostics;

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

    public async Task<CartViewModel> AddProductToCartAsync(int productId, int amount)
    {
        var product = await _context.Products.FindAsync(productId);

        if (product != null)         
        {
            var cartViewModel = await GetCartViewModelAsync(); 
            var cartItem = cartViewModel.CartItems.FirstOrDefault(
                item => item.ProductId == productId
             );

            if (cartItem != null)
                cartItem.Amount += amount;
            else
            {
                cartViewModel.CartItems.Add(
                    new CartItemViewModel
                    {
                        ProductId = product.ProductId,
                        Name = product.Name  ,
                        Price = product.Price ,
                        Amount = amount

                    });
            }

            cartViewModel.Total = cartViewModel.CartItems.Sum(item => item.Amount * item.Price);

            await UpdateCartViewModelAsync(cartViewModel);
            return cartViewModel;

        }
        return new CartViewModel();

    }

    private async Task UpdateCartViewModelAsync(CartViewModel cartViewModel)
    {
       var productIds= cartViewModel.CartItems.Select(item => 
         new ProductIdAmount
         {
             ProductId= item.ProductId,
             Amount =  item.Amount
         }
       
       ).ToList();

        var cartJson = await Task.Run(() => JsonConvert.SerializeObject(productIds));

        Response.Cookies.Append("cart", cartJson,
            new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddDays(7),
            }
        );
    }

    private async Task<CartViewModel> GetCartViewModelAsync()
    {
        var cartJson = Request.Cookies["cart"];

        if (string.IsNullOrEmpty(cartJson))
            return new CartViewModel();    

        var productIdsAmount  = JsonConvert.DeserializeObject<List<ProductIdAmount>>(cartJson);

        var cartViewModel = new CartViewModel();

        if (productIdsAmount != null)
        {
            foreach (var item in productIdsAmount)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                if (product != null)
                {
                    cartViewModel.CartItems.Add(
                        new CartItemViewModel 
                        {
                            ProductId = product.ProductId, 
                            Name =product.Name,
                            Price = product.Price,
                            Amount= item.Amount
                        });
                }
            }
        }
        cartViewModel.Total = cartViewModel.CartItems.Sum(item => item.SubTotal);
        return cartViewModel;
    }






    //---------ERRORS---------------
    protected IActionResult HandleError(Exception e)
    {
        return View("Error", new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
        });
    }

    protected IActionResult HandleDbError(DbException dbException) 
    {

        var viewModel = new DbErrorViewModel
        {
            ErrorMessage = "Error in the database ",
            ErrorDetail = dbException.Message
        };

        return View("DbError", viewModel);    
    }

    protected IActionResult HandleDbUpdateError(DbUpdateException dbUpdateException)
    {

        var viewModel = new DbErrorViewModel
        {
            ErrorMessage = "Database update error",
            ErrorDetail = dbUpdateException.Message
        };

        return View("DbError", viewModel);
    }

}



