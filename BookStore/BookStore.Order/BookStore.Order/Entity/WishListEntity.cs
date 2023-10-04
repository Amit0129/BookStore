using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Order.Entity
{
    public class WishListEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long WishListID { get; set; }
        [Required]
        //[RegularExpression("^[0-9]{1,}$")]
        public long BookID { get; set; }
        [Required]
        //[RegularExpression("^[0-9]{1,}$")]
        public long UserID { get; set; }
        [NotMapped]
        public BookEntity Book { get; set; }
        [NotMapped]
        public UserEntity User { get; set; }
    }
}
