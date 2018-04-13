using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Program
{
    static void Main(string[] args)
    {
        Pizza pizza = new Pizza();
        Dough dough = new Dough();
        //var rawDough = new List<string>();
        //var listOfToppingCommands = new List<string>();
        var toppingList = new List<Topping>();
        //string pizzaName;
        string command;
        //while ((command = Console.ReadLine()) != "END")
        //{
        //    var rawCommands = command.Split();
        //    if (rawCommands[0].ToLower() == "pizza")
        //    {
        //        try
        //        {
        //            pizza.PizzaName = rawCommands[1];
        //        }
        //        catch (Exception e)
        //        {
        //
        //            Console.WriteLine(e.Message);
        //        }
        //        
        //    }
        //    else if (rawCommands[0].ToLower() == "dough")
        //    {
        //        rawDough.Add(rawCommands[1]);
        //        rawDough.Add(rawCommands[2]);
        //        rawDough.Add(rawCommands[3]);
        //    }
        //    else if (rawCommands[0].ToLower() == "topping")
        //    {
        //        listOfToppingCommands.Add(command);
        //    }
        //}
        //
        //foreach (var c in listOfToppingCommands)
        //{
        //    var commandArgs = c.Split();
        //    try
        //    {
        //        Topping singleTopping = new Topping(commandArgs[1], int.Parse(commandArgs[2]));
        //        toppingList.Add(singleTopping);
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //        return;
        //    }
        //}
        //if (toppingList.Count > 10)
        //{
        //    Console.WriteLine("Number of toppings should be in range [0..10].");
        //    return;
        //}
        //
        //try
        //{
        //    Dough newDough = new Dough(rawDough[0], rawDough[1], int.Parse(rawDough[2]));
        //    dough = newDough;
        //}
        //catch (Exception e)
        //{
        //    Console.WriteLine(e.Message);
        //    return;
        //}




        while ((command = Console.ReadLine()) != "END")
        {
            var commandArgs = command.Split();
            if (commandArgs[0] == "Pizza")
            {
                try
                {
                    pizza.PizzaName = commandArgs[1];
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return;
                }
            }
            else if (commandArgs[0] == "Dough")
            {
                try
                {
                    var flourType = commandArgs[1];
                    var bakingTechnique = commandArgs[2];
                    var doughWeigth = int.Parse(commandArgs[3]);
                    dough = new Dough(flourType, bakingTechnique, doughWeigth);
                }
                catch (Exception e)
                {
        
                    Console.WriteLine(e.Message);
                    return;
                }
            }
            else if (commandArgs[0] == "Topping")
            {
                try
                {
                    var toppingType = commandArgs[1];
                    var toppingWeigth = int.Parse(commandArgs[2]);
                    var topping = new Topping(toppingType, toppingWeigth);
                    toppingList.Add(topping);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return;
                }
        
            }
        }
        try
        {
            pizza.ToppingList = toppingList;
            pizza.Dough = dough;
        }
        catch (Exception e)
        {

            Console.WriteLine(e.Message);
            return;
        }
        
        double calories = 0;
        foreach (var topping in pizza.ToppingList)
        {
            calories += topping.ToppingCalories;
        }
        calories += pizza.Dough.Calories;

        Console.WriteLine($"{pizza.PizzaName} - { calories:f2} Calories.");
    }
}
