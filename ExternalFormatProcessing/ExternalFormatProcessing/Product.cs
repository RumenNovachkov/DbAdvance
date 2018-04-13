using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExternalFormatProcessing
{
    public class Product
    {
        public string Name { get; set; }

        public string Description { get; set; }
        
        public int? ManufacturerId { get; set; }
    }
}
