using Business.DTOs;

namespace Business.ViewModels;

public class CartItemViewModel
{

    public int ProductId { get; set; }
    public ProductDto Product { get; set; } = null!;
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public int Amount { get; set; }
    public decimal SubTotal => Price * Amount;

}
