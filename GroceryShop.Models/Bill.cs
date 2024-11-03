namespace GroceryShop.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }

    public class Bill
    {
        public string CustomerEmail { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();
        public double TotalAmount { get; set; }
    }

    public class Order
    {
        public string CustomerEmail { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();
    }
}
