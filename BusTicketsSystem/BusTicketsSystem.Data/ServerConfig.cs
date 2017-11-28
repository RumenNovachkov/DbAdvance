using System;

namespace BusTicketsSystem.Data
{
    public class ServerConfig
    {
        public static string ConnectionString { get; set; } = "Server=.;Database=BusTicketsSystem;Integrated Security=True";
    }
}
