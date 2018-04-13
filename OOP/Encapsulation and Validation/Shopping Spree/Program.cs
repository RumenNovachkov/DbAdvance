using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Program
{
    static void Main()
    {
        var persons = new Dictionary<string, Person>();
        var personsInput = Console.ReadLine().Split(';');
        foreach (var p in personsInput)
        {
            if (p == string.Empty)
            {
                break;
            }
            else
            {
                var singlePersonArgs = p.Split('=');
                string name = singlePersonArgs[0];
                decimal money = decimal.Parse(singlePersonArgs[1]);
                try
                {
                    Person singlePerson = new Person(name, money);
                    if (!persons.ContainsKey(name))
                    {
                        persons.Add(name, singlePerson);
                    }
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                    return;
                }
            }
        }

        var products = new Dictionary<string, Product>();
        var productsInput = Console.ReadLine().Split(';');
        foreach (var p in productsInput)
        {
            if (p == string.Empty)
            {
                break;
            }
            else
            {
                var singleProductArgs = p.Split('=');
                string productName = singleProductArgs[0];
                decimal price = decimal.Parse(singleProductArgs[1]);
                try
                {
                    Product singleProduct = new Product(productName, price);
                    if (!products.ContainsKey(productName))
                    {
                        products.Add(productName, singleProduct);
                    }
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                    return;
                }
                
            }
        }

        string command;
        while ((command = Console.ReadLine()) != "END")
        {
            var commandArgs = command.Split();
            var client = commandArgs[0];
            var item = commandArgs[1];
            var cost = products[item].Price;
            try
            {
                persons[client].ShoppingProducts(item, cost);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                continue;
            }
            
        }

        foreach (var person in persons)
        {
            Console.WriteLine(person.Value.ToString());
        }
    }
}
