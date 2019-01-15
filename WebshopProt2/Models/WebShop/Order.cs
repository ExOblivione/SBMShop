using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace WebshopProt2.Models.WebShop
{
    public partial class Order
    {
        [ScaffoldColumn(false)]
        public int OrderId { get; set; }

        [ScaffoldColumn(false)]
        public System.DateTime OrderDate { get; set; }

        [ScaffoldColumn(false)]
        public string Username { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [DisplayName("First Name")]
        [StringLength(160)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [DisplayName("Last Name")]
        [StringLength(160)]
        public string LastName { get; set; }

        [DataMember(IsRequired = true)]
        [Required(ErrorMessage = "Address is required")]
        [StringLength(70)]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required")]
        [StringLength(40)]
        public string City { get; set; }

        [Required(ErrorMessage = "Postal Code is required")]
        [DisplayName("Postal Code")]
        [StringLength(10)]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Country is required")]
        [StringLength(40)]
        public string Country { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [StringLength(24)]
        public string Phone { get; set; }



        [Display(Name = "Credit Card")]
        [NotMapped]
        [Required]
        [DataType(DataType.CreditCard)]
        public String CreditCard { get; set; }

        [Display(Name = "Credit Card Type")]
        [NotMapped]
        public String CcType { get; set; }

        [Display(Name = "Expiration Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Experation { get; set; }



        public bool SaveInfo { get; set; }


        [DisplayName("Email Address")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
            ErrorMessage = "Email is is not valid.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [ScaffoldColumn(false)]
        public decimal Total { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }



        public string ToString(Order order)
        {
            StringBuilder mindy = new StringBuilder();

            mindy.Append("<p>Order Information for Order: " + order.OrderId + "<br>Placed at: " + order.OrderDate + "</p>").AppendLine();
            mindy.Append("<p>Name: " + order.FirstName + " " + order.LastName + "<br>");
            mindy.Append("Address: " + order.Address + " " + order.City + " " + order.PostalCode + "<br>");
            mindy.Append("Contact: " + order.Email + "     " + order.Phone + "</p>");
            mindy.Append("<p>Charge: " + order.CreditCard + " " + order.Experation.ToString("dd-MM-yyyy") + "</p>");
            mindy.Append("<p>Credit Card Type: " + order.CcType + "</p>");

            mindy.Append("<br>").AppendLine();
            mindy.Append("<Table>").AppendLine();
            // Display header 
            string header = "<tr> <th>Item Name</th>" + "<th>Quantity</th>" + "<th>Price</th> <th></th> </tr>";
            mindy.Append(header).AppendLine();

            String output = String.Empty;
            try
            {
                foreach (var item in order.OrderDetails)
                {
                    mindy.Append("<tr>");
                    output = "<td>" + item.Item.Author + "</td>" + "<td>" + item.Quantity + "</td>" + "<td>" + item.Quantity * item.Price + "</td>";
                    mindy.Append(output).AppendLine();
                    Console.WriteLine(output);
                    mindy.Append("</tr>");
                }
            }
            catch (Exception)
            {
                output = "No items ordered.";
            }
            mindy.Append("</Table>");
            mindy.Append("<b>");
            // Display footer 
            string footer = String.Format("{0,-12}{1,12}\n",
                                          "Total", order.Total);
            mindy.Append(footer).AppendLine();
            mindy.Append("</b>");

            return mindy.ToString();
        }
    }
}