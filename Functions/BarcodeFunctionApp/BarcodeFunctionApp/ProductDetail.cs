using System;

namespace BarcodeFunctionApp
{
    public class ProductDetail
    {
        public string Id { get; set; }

        public DateTime Created { get; set; }

        public string CreatedBy { get; set; }

        public DateTime Modified { get; set; }

        public string ModifiedBy { get; set; }

        public string Name { get; set; }
        public string BarCode { get; set; }
        public double SalePrice { get; set; }
    }
}