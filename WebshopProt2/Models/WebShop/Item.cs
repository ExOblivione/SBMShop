using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Web.Mvc.Html;

namespace WebshopProt2.Models.WebShop
{
    public class Item
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        [Key]
        [ScaffoldColumn(false)]
        public int ID { get; set; }

        [DisplayName("Category")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "The book's ISBN is required")]
        public string ISBN { get; set; }

        [Required(ErrorMessage = "The book's Author is required")]
        [StringLength(160)]
        public string Author { get; set; }

        [Required(ErrorMessage = "The book's Title is required")]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 999.99, ErrorMessage = "Price must be between 0.01 and 999.99")]
        public decimal Price { get; set; }

        public byte[] InternalImage { get; set; }



        [Display(Name = "Image Pick")]
        [NotMapped]
        public HttpPostedFileBase File
        {
            get
            {
                return null;
            }
            set
            {
                try
                {
                    MemoryStream target = new MemoryStream();
                    if (value.InputStream == null)
                        return;

                    value.InputStream.CopyTo(target);
                    InternalImage = target.ToArray();
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    logger.Error(ex.StackTrace);
                }
            }
        }
        
        [DisplayName("Image URL")]
        [StringLength(1024)]
        public string ItemPictureUrl { get; set; }



        public virtual Categories Categories { get; set; }

        public virtual List<OrderDetails> OrderDetails { get; set; }
    }
}