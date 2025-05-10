using B07ASPC11_Ecommerce.Data;

namespace B07ASPC11_Ecommerce.ViewModels
{
    public class Cart
    {
        public Product Product { get; set; } = new Product();
        public int Qty { get; set; }
    }
}
