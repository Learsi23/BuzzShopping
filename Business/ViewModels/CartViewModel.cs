using Business.DTOs;

namespace Business.ViewModels;

public class CartViewModel
{
    public List<CartItemViewModel> CartItems { get; set; } = new(); 
    public decimal Total { get; set; }           

}
