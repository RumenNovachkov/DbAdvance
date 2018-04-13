using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Box
{
    private double length;
    private double width;
    private double height;

    public Box(double length, double width, double heigth)
    {
        this.Length = length;
        this.Width = width;
        this.Height = heigth;
    }

    public double Surface(double length, double width, double heigth)
    {
        var boxSurface = (2 * this.length) + (2 * this.width) + (2 * this.heigth);
        return boxSurface;
    }

    public double Length
    {
        get
        {
            return this.length;
        }
        set
        {
            this.length = value;
        }
    }

    public double Width
    {
        get
        {
            return this.width;
        }
        set
        {
            this.width = value;
        }
    }

    public double Height
    {
        get
        {
            return this.height;
        }
        set
        {
            this.height = value;
        }
    }
}
