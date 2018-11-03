using System.Collections.Generic;

namespace dotnetKonf.Web.Models
{
    public class PriceModel
    {
        public List<Pricing> Pricings { get; set; }
    }

    public class Pricing
    {
        public decimal Price { get; set; }
    }
}