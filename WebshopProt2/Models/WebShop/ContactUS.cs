using Acme.Shared.Contracts;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;


namespace WebshopProt2.Models.WebShop
{
    [DataContract]
    public class ContactUS
    {
        [DataMember(IsRequired = true)]
        [Key]
        [ScaffoldColumn(false)]
        public int ContactID { get; set; }

        [DataMember(IsRequired = true)]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [DataMember(IsRequired = true)]
        public string Message { get; set; }

        [DataMember(IsRequired = true)]
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateofMessage { get; set; }
    }
}