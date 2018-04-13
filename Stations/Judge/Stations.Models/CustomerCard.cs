namespace Stations.Models
{
    using System;
    using System.Collections.Generic;

    public class CustomerCard
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age
        {
            get
            {
                return this.Age;
            }
            set
            {
                if(value < 0 || value > 128)
                {
                    throw new ArgumentException("Invalid value");
                }
                this.Age = value;
            }
        }

        public CardType Type { get; set; }

        public ICollection<Ticket> BoughtTickets { get; set; } = new List<Ticket>();
    }
}
