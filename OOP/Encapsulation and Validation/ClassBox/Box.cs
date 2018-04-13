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

    public Box(double length, double width, double height)
    {
        this.Length = length;
        this.Width = width;
        this.Height = height;
    }

    public string SurfaceAre()
    {
        var surfceArea = (2 * this.Length * this.Width) + (2 * this.Length * this.Height) + (2 * this.Width * this.Height);
        return $"Surface Area - {surfceArea:f2}";
    }

    public string LateralSurfaceAre()
    {
        var lateralSurfceArea = (2 * this.Length * this.Height) + (2 * this.Width * this.Height);
        return $"Lateral Surface Area - {lateralSurfceArea:f2}";
    }

    public string Volume()
    {
        var volume = this.Length * this.Width * this.Height;
        return $"Volume - {volume:f2}";
    }

    private double Length
    {
        get 
        {
            return this.length;
        }
        set 
        {
            if (value <= 0)
            {
                throw new AggregateException("Length cannot be zero or negative.");
            }
            else this.length = value;
        }
    }

    private double Width
    {
        get
        {
            return this.width;
        }
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Width cannot be zero or negative.");
            }
            else this.width = value;
        }
    }

    private double Height
    {
        get
        {
            return this.height;
        }
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Height cannot be zero or negative.");
            }
            else this.height = value;
        }
    }
}
