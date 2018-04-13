using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Person
{
    private string name;
    private decimal money;
    private List<string> bag;

    public Person(string name, decimal money)
    {
        this.Name = name;
        this.Money = money;
        this.Bag = new List<string>();
    }

    public void ShoppingProducts(string product, decimal cost)
    {
        if (this.Money >= cost)
        {
            this.Money -= cost;
            this.Bag.Add(product);
            Console.WriteLine($"{this.Name} bought {product}");
        }
        else
        {
            throw new ArgumentException($"{this.Name} can't afford {product}");
        }
    }

    public override string ToString()
    {
        string bagMessage;
        if (this.Bag.Count == 0)    bagMessage = "Nothing bought";
        else                        bagMessage = string.Join(", ", this.Bag);
        var message = $"{this.Name} - " + bagMessage;
        return message;
    }

    public string Name
    {
        get
        {
            return this.name;
        }
        set
        {
            if (value == string.Empty)      throw new ArgumentException("Name cannot be empty");
            else                            this.name = value;
        }
    }

    public decimal Money
    {
        get
        {
            return this.money;
        }
        set
        {
            if (value < 0)     throw new ArgumentException("Money cannot be negative");
            else                this.money = value;
        }
    }

    public List<string> Bag
    {
        get
        {
            return this.bag;
        }
        set
        {
            this.bag = value;
        }
    }
}
