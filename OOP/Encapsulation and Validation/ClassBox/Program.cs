using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

public class Program
{
    static void Main(string[] args)
    {
        var length = double.Parse(Console.ReadLine());
        var width = double.Parse(Console.ReadLine());
        var height = double.Parse(Console.ReadLine());
        Console.WriteLine(3);
        try
        {
            Box box = new Box(length, width, height);
            Console.WriteLine(box.SurfaceAre());
            Console.WriteLine(box.LateralSurfaceAre());
            Console.WriteLine(box.Volume());
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
