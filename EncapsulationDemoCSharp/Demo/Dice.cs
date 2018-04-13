using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Dice
{
    private Random random = new Random();
    private int sides;

    public Dice(int sides)
    {
        this.Sides = sides;
    }

    public int Sides
    {
        get
        {
            return this.sides;
        }
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Shibay se!");
            }
            this.sides = value;
        }
    }

    public int Roll()
    {
        var rolledResult = this.random.Next(1, this.Sides + 1);
        return rolledResult;
    }
}
