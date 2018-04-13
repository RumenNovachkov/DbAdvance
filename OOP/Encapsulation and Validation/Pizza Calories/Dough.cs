using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Dough
{
    private string flourType;
    private string bakingTechnique;
    private int doughWeigth;
    private double calories;

    public Dough() { }

    public Dough(string flourType, string bakingTechnique, int doughWeigth)
    {
        this.FlourType = flourType.ToLower();
        this.BakingTechnique = bakingTechnique.ToLower();
        this.DoughWeigth = doughWeigth;
        this.Calories = 2 * doughWeigth;

        if (flourType.ToLower() == "white")                   this.Calories *= 1.5;
        else if (flourType.ToLower() == "wholegrain")         this.Calories *= 1.0;

        if (bakingTechnique.ToLower() == "crispy")            this.Calories *= 0.9;
        else if (bakingTechnique.ToLower() == "chewy")        this.Calories *= 1.1;
        else if (bakingTechnique.ToLower() == "womemade")     this.Calories *= 1.0;
    }

    public string FlourType
    {
        get
        {
            return this.flourType;
        }
        set
        {
            if (value == "white")
            {
                this.flourType = value;
            }

            else if (value == "wholegrain")
            {
                this.flourType = value;
            }
            else
            {
                throw new ArgumentException("Invalid type of dough.");
            }

        }
    }

    public string BakingTechnique
    {
        get
        {
            return this.bakingTechnique;
        }
        set
        {
            if (value == "crispy")
            {
                this.bakingTechnique = value;
            }
            else if (value == "chewy")
            {
                this.bakingTechnique = value;
            }
            else if (value == "homemade")
            {
                this.bakingTechnique = value;
            }
            else
            {
                throw new ArgumentException("Invalid baking technique.");
            }
        }
    }

    public int DoughWeigth
    {
        get
        {
            return this.doughWeigth;
        }
        set
        {
            if (value > 0 && value <= 200)
            {
                this.doughWeigth = value;
            }
            else
            {
                throw new ArgumentException("Dough weight should be in the range [1..200].");
            }
            
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
            this.calories = value;
        }
    }
}
