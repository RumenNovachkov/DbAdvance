using System;
using System.Collections.Generic;
using System.Text;

namespace AllInOffice.Data
{
    public class Configuration
    {
        public static string ConnectionString { get; set; } = @"Server=RZR\SQLEXPRESS;Database=AllInOffice;Integrated Security=True";
    }
}
