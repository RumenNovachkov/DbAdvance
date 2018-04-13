using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Book
{
    private string title;
    private string author;
    private decimal price;

    public Book(string author, string title, decimal price)
    {
        this.Title = title;
        this.Author = author;
        this.Price = price;
    }

    public virtual string Title
    {
        get
        {
            return this.title;
        }
        set
        {
            if (value.Length < 3)
            {
                throw new ArgumentException("Title not valid!");
            }
            else             this.title = value;
        }
    }

    public virtual string Author
    {
        get
        {
            return this.author;
        }
        set
        {
            var nameArgs = value.Split();
            string secondName;
            if (nameArgs.Length > 1)
            {
                secondName = nameArgs[1];
                bool isDigit = Convert.ToChar(secondName.First()) <= 57 && Convert.ToChar(secondName.First()) >= 48;
                if (isDigit)
                {
                    throw new ArgumentException("Author not valid!");
                }
                else this.author = value;
            }
            else
            {
                this.author = value;
            }
            
            
        }
    }

    public virtual decimal Price
    {
        get
        {
            return this.price;
        }
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Price not valid!");
            }
            else             this.price = value;
        }
    }

    public override string ToString()
    {
        return $"Type: {this.GetType().Name}" + Environment.NewLine +
            $"Title: {this.Title}" + Environment.NewLine +
            $"Author: {this.Author}" + Environment.NewLine +
            $"Price: {this.Price:f2}";
    }

}
