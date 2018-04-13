using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Topping
{
    private string toppingType;
    private int toppingWeigth;
    private double toppingCalories;

    public Topping() { }

    public Topping(string toppingType, int toppingWeigth)
    {
        this.ToppingType = toppingType;
        this.ToppingWeigth = toppingWeigth;
        this.ToppingCalories = 2 * toppingWeigth;
        if (toppingType.ToLower() == "meat") this.ToppingCalories *= 1.2;
        else if (toppingType.ToLower() == "veggies") this.ToppingCalories *= 0.8;
        else if (toppingType.ToLower() == "cheese") this.ToppingCalories *= 1.1;
        else if (toppingType.ToLower() == "sauce") this.ToppingCalories *= 0.9;
    }

    public string ToppingType
    {
        get
        {
            return this.toppingType;
        }
        set
        {
            if (value.ToLower() == "meat")
            {
                this.toppingType = value;
            }
            else if (value.ToLower() == "veggies")
            {
                this.toppingType = value;
            }
            else if (value.ToLower() == "cheese")
            {
                this.toppingType = value;
            }
            else if (value.ToLower() == "sauce")
            {
                this.toppingType = value;
            }
            else    throw new ArgumentException($"Cannot place {value} on top of your pizza.");
        }
    }

    public int ToppingWeigth
    {
        get
        {
            return this.toppingWeigth;
        }
        set
        {
            if (value > 0 && value <= 50)
            {
                this.toppingWeigth = value;
            }
            else    throw new ArgumentException($"{this.ToppingType} weight should be in the range [1..50]");
        }
    }

    public double ToppingCalories
    {
        get
        {
            return this.toppingCalories;
        }
        set
        {
            this.toppingCalories = value;
        }
    }
}
