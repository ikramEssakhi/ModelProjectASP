namespace ModelAsp1.Models
{
    public class Cart
    {


        public List<CartLine> Items { get; set; } //panier composé de 1..* items

        public Cart()
        {
            Items = new List<CartLine>();
        }

        public void AddItem(Product product, int quantity)
        {
            var existingItem = Items.FirstOrDefault(item => item.ProductId == product.Id);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity; // si l item existe deja dans le panier on augmente que sa nouvelle quantité
            }
            else//sinon on ajoute un nouveu item dans le panier avec la quantité specifiée lors de l'ajout dans le panier
            {
                var newItem = new CartLine
                {
                    ProductId = product.Id,
                    ProductName = product.Title,
                    Price = product.Price,
                    Quantity = quantity
                };
                Items.Add(newItem);
            }
        }

        public void RemoveItem(int productId)
        {
            // remove items from panier
            var itemToRemove = Items.FirstOrDefault(item => item.ProductId == productId);

            if (itemToRemove != null)
            {
                Items.Remove(itemToRemove);
            }
        }
    }
}
