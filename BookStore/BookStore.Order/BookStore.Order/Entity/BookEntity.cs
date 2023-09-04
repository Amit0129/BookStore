using System.ComponentModel.DataAnnotations;

namespace BookStore.Order.Entity
{
    public class BookEntity
    {
        public long BookID { get; set; }
        public string BookName { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public int Quantity { get; set; }
        public float DiscountPrice { get; set; }
        public float ActualPrice { get; set; }
    }
}
