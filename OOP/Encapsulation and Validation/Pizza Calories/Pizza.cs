using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Pizza
{
    public string pizzaName;
    public Dough dough;
    public List<Topping> toppingList;
    public double calories;

    public Pizza() { }

    public Pizza(string pizzaName)
    {
        this.PizzaName = pizzaName;
    }

    public Pizza(Dough dough)
    {
        this.Dough = dough;
    }

    public Pizza(List<Topping> toppingList)
    {
        this.ToppingList = toppingList;
    }

    public string PizzaName
    {
        get
        {
            return this.pizzaName;
        }
        set
        {
            if (value == string.Empty || value.Length > 15)
            {
                throw new ArgumentException("Pizza name should be between 1 and 15 symbols.");
            }
            else this.pizzaName = value;
        }
    }

    public Dough Dough
    {
        get
        {
            return this.dough;
        }
        set
        {
            this.dough = value;
        }
    }

    public List<Topping> ToppingList
    {
        get
        {
            return this.toppingList;
        }
        set
        {
            if (value.Count < 0 || value.Count > 10)
            {
                throw new ArgumentException("Number of toppings should be in range [0..10].");
            }
            else this.toppingList = value;
        }
    }

    public double Calories
    {
        get
        {
            return this.calories;
        }
        set
        {
            foreach (var topping in ToppingList)
            {
                this.calories += topping.ToppingCalories;
            }
            this.calories += this.Dough.Calories;
        }
    }
}
