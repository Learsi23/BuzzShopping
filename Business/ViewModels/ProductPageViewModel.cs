using Business.DTOs;

namespace Business.ViewModels
{
    public class ProductPageViewModel
    {
        public List<ProductDto> Products { get; set; } = null!;
        public int ActualPage { get; set; }
        public int TotalPage { get; set; }
        public int? CategoryIdSelected { get; set; }
        public string? Search { get; set; } 
        public bool ShowNoResultsMessage { get; set; }
        public string? CategoryNameSelected { get; set; }
    }
}
