using GroceryShop.Models;
public class InventoryService {
    private List<Item> _items = new List<Item>{
        new Item { Id = 1, Name = "Apple", Price = 1.00 },
        new Item { Id = 2, Name = "Banana", Price = 0.50 },
        new Item { Id = 3, Name = "Orange", Price = 1.50}
    };

    public Item GetItem(int id) => _items.FirstOrDefault(x => x.Id == id);

    public void ReduceItem(int itemId, int quantity) {
        var item = _items.FirstOrDefault(x => x.Id == itemId);
        if (item != null) {
            item.Quantity -= quantity;
        }
    }
}