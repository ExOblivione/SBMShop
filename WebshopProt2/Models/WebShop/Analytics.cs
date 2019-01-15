using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebshopProt2.ViewModels;

namespace WebshopProt2.Models.WebShop
{
    public class AnalyticsViewModel
    {
        public List<OrderDateGroup> OrderData { get; set; }
        public List<OrderDateGroup> OrderDataForToday { get; set; }
    }
}