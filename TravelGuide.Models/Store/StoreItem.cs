using System;

namespace TravelGuide.Models.Store
{
    public class StoreItem
    {
        public StoreItem()
        {
            this.Id = Guid.NewGuid();
            this.IsDeleted = false;
            this.CreatedOn = DateTime.Now;
            this.InStock = true;
        }

        public StoreItem(string itemName, string description, string destFor, string imageUrl, string brand, double price)
            : this()
        {
            this.ItemName = itemName;
            this.Description = description;
            this.DestinationFor = destFor;
            this.ImageUrl = imageUrl;
            this.Brand = brand;
            this.Price = price;
        }

        public Guid Id { get; set; }

        public string ItemName { get; set; }

        public string Description { get; set; }

        public string DestinationFor { get; set; }

        public string ImageUrl { get; set; }

        public string Brand { get; set; }

        public double Price { get; set; }

        public bool InStock { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
