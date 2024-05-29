namespace Fiorello_MVC.ViewModels.Baskets
{
    public class TotalBasketVM
    {
        public List<CartVM> Products { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
