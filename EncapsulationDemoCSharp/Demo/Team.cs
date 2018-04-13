using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Team
{
    private List<Person> players;

    public Team()
    {
        this.Players = new List<Person>();
    }

    public List<Person> Players
    {
        get
        {
            return this.players;
        }
        private set
        {
            this.players = value;
        }
    }
}
