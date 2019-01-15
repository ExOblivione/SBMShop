using System.ComponentModel.DataAnnotations;

namespace WebshopProt2.Models.WebShop
{
    public class Cart
    {
        [Key]
        public int ID { get; set; }
        public string CartID { get; set; }
        public int ItemID { get; set; }
        public int Count { get; set; }
        public System.DateTime DateCreated { get; set; }
        public virtual Item Item { get; set; }

    }
}