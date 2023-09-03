using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Books.Entity
{
    public class BookEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long BookID { get; set; }


        [Required]
        public string BookName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public float DiscountPrice { get; set; }

        [Required]
        public float ActualPrice { get; set;}
    }
}
