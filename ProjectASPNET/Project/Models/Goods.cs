namespace Project.Models
{
    public class Goods
    {
        public int Id { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string Producer {  get; set; }
        public int NumberOf {  get; set; }
        public double Price { get; set; }
        public double OrderPrice { get; set; }
        public int NumberOfPrices { get; set; }
        public double PriceAllOrders { get; set; }
        public string Category { get; set; }
        public string Subcategory {  get; set; }
    }
}
