using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Order.Entity
{
    public class OrderEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long OrderID { get; set; }


        [Required]
        public long UserID { get; set; }


        [Required]
        public long BookID { get; set; }


        [Required]
        public int OrderQty { get; set; }


        [NotMapped]
        public float OrderAmount { get; set; }


        [NotMapped]
        public BookEntity Book { get; set; }


        [NotMapped]
        public UserEntity User { get; set; }
    }
}
