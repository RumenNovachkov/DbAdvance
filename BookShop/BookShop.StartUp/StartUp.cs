namespace BookShop
{
    using BookShop.Data;
    using BookShop.Initializer;
    using BookShop.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    public class StartUp
    {
        static void Main()
        {
            using (var db = new BookShopContext())
            {
                //DbInitializer.ResetDatabase(db);

                //1.
                //var command = Console.ReadLine();
                //
                //Console.WriteLine(GetBooksByAgeRestriction(db, command));

                //2.
                //Console.WriteLine(GetGoldenBooks(db));

                //3.
                //Console.WriteLine(GetBooksByPrice(db));

                //4.
                //var year = int.Parse(Console.ReadLine());
                //
                //Console.WriteLine(GetBooksNotRealeasedIn(db, year));

                //5.
                //var input = Console.ReadLine();
                //
                //var result = GetBooksByCategory(db, input);
                //
                //Console.WriteLine(result);

                //6.
                //var date = Console.ReadLine();
                //
                //Console.WriteLine(GetBooksReleasedBefore(db, date));

                //7.
                //var input = Console.ReadLine();
                //
                //Console.WriteLine(GetAuthorNamesEndingIn(db, input));

                //8.
                //var input = Console.ReadLine();
                //
                //Console.WriteLine(GetBookTitlesContaining(db, input));

                //9.
                //var input = Console.ReadLine();
                //
                //Console.WriteLine(GetBooksByAuthor(db, input));

                //10.
                //var input = int.Parse(Console.ReadLine());
                //
                //Console.WriteLine(CountBooks(db, input));

                //11.
                //Console.WriteLine(CountCopiesByAuthor(db));

                //12.
                //Console.WriteLine(GetTotalProfitByCategory(db));

                //13.
                //Console.WriteLine(GetMostRecentBooks(db));

                //14.
                //IncreasePrices(db);

                //15.
                //Console.WriteLine(RemoveBooks(db) + " books were deleted");

            }
        }

        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            int enumValue = -1;

            switch (command.ToLower())
            {
                case "minor":
                    enumValue = 0;
                    break;
                case "teen":
                    enumValue = 1;
                    break;
                case "adult":
                    enumValue = 2;
                    break;
                default:
                    break;
            }

            string[] titles = context
                .Books
                .Where(b => b.AgeRestriction == (AgeRestriction)enumValue)
                .Select(b => b.Title)
                .OrderBy(bt => bt)
                .ToArray();

            var result = string.Join(Environment.NewLine, titles);

            return result;
        } //1.

        public static string GetGoldenBooks(BookShopContext db)
        {
            var goldenbooks = db
                .Books
                .Where(b => b.Copies < 5000 && b.EditionType == EditionType.Gold)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToArray();

            var result = string.Join(Environment.NewLine, goldenbooks);

            return result;
        } //2.

        public static string GetBooksByPrice(BookShopContext db)
        {
            var booksByPrice = db
                .Books
                .Select(b => new
                {
                    Title = b.Title,
                    Price = b.Price
                })
                .Where(b => b.Price > 40)
                .OrderByDescending(b => b.Price)
                .ToArray();

            var booksWithPrice = new List<string>();

            foreach (var b in booksByPrice)
            {
                booksWithPrice.Add($"{b.Title} - ${b.Price:f2}");
            }

            var result = string.Join(Environment.NewLine, booksWithPrice);

            return result;
        } //3.

        public static string GetBooksNotRealeasedIn(BookShopContext db, int year)
        {
            var notReleasedInBooks = db
                .Books
                .Where(b => (int)b.ReleaseDate.Value.Year != year)
                .Select(b => b.Title)
                .ToArray();

            var result = string.Join(Environment.NewLine, notReleasedInBooks);

            return result;
        } //4.

        public static string GetBooksByCategory(BookShopContext db, string input)
        {
            var categories = input.ToLower().Split(new[] { "\t", " ", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToArray();

            var titles = db.Books
                .Where(b => b.BookCategories.Any(c => categories.Contains(c.Category.Name.ToLower())))
                .Select(b => b.Title)
                .OrderBy(t => t)
                .ToArray();

            var result = string.Join(Environment.NewLine, titles);

            return result;

        } //5.*Any()

        public static string GetBooksReleasedBefore(BookShopContext db, string dateString)
        {
            var date = DateTime.ParseExact(dateString, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            var books = db.Books
                .Where(b => b.ReleaseDate < date)
                .OrderByDescending(b => b.ReleaseDate)
                .Select(b => new
                {
                    Title = b.Title,
                    Type = b.EditionType,
                    Price = b.Price
                }).ToArray();

            var booksCollection = new List<string>();

            foreach (var book in books)
            {
                booksCollection.Add($"{book.Title} - {book.Type} - ${book.Price:f2}");
            }

            var result = string.Join(Environment.NewLine, booksCollection);

            return result;
        } //6.

        public static string GetAuthorNamesEndingIn(BookShopContext db, string input)
        {
            var authors = db.Authors
                .Where(a => a.FirstName.EndsWith(input))
                .Select(a => new
                {
                    FirstName = a.FirstName,
                    LastName = a.LastName
                })
                .OrderBy(a => a.FirstName)
                .ThenBy(a => a.LastName)
                .ToArray();

            var authorsList = new List<string>();

            foreach (var a in authors)
            {
                authorsList.Add($"{a.FirstName} {a.LastName}");
            }

            var result = string.Join(Environment.NewLine, authorsList);

            return result;

        } //7.

        public static string GetBookTitlesContaining(BookShopContext db, string input)
        {
            var books = db.Books
                .Where(b => b.Title.ToLower().Contains(input.ToLower()))
                .Select(b => b.Title)
                .OrderBy(t => t)
                .ToArray();

            var result = string.Join(Environment.NewLine, books);

            return result;

        } //8.

        public static string GetBooksByAuthor(BookShopContext db, string input)
        {
            var books = db.Books
                .Where(b => b.Author.LastName.ToLower().StartsWith(input.ToLower()))
                .OrderBy(b => b.BookId)
                .Select(b => b.Title + " (" + b.Author.FirstName + " " + b.Author.LastName + ")")
                .ToArray();

            var result = string.Join(Environment.NewLine, books);

            return result;

        } //9.

        public static int CountBooks(BookShopContext db, int lengthCheck)
        {
            var result = db.Books.Where(b => b.Title.Length > lengthCheck).Count();

            //return $"There are {result} books with longer title than {lengthCheck} symbols";
            return result;
        }//10.

        public static string CountCopiesByAuthor(BookShopContext db)
        {
            var authors = new Dictionary<int, int>();

            foreach (var b in db.Books)
            {
                if (!authors.ContainsKey(b.AuthorId))
                {
                    authors.Add(b.AuthorId, b.Copies);
                }
                else
                    authors[b.AuthorId] += b.Copies;
            }

            var counts = new List<string>();

            foreach (var id in authors.OrderByDescending(c => c.Value))
            {
                var author = db.Authors.SingleOrDefault(a => a.AuthorId == id.Key);
                counts.Add($"{author.FirstName} {author.LastName} - {id.Value}");
            }

            var result = string.Join(Environment.NewLine, counts);

            return result;


        }//11. Mnogo grozen kod!

        public static string GetTotalProfitByCategory(BookShopContext db)
        {

            var cat = db.Categories
                .Select(c => new
                {
                    Name = c.Name,
                    Profit = c.CategoryBooks.Select(b => b.Book.Copies * b.Book.Price).Sum()
                })
                .OrderByDescending(c => c.Profit)
                .ToArray();

            var builder = new StringBuilder();

            foreach (var c in cat)
            {
                builder.Append(c.Name + " $" + c.Profit + Environment.NewLine);
            }

            return builder.ToString();

        } //12. Ot lekciqta

        public static string GetMostRecentBooks(BookShopContext db)
        {
            var categories = db.Categories
                .OrderBy(c => c.Name)
                .Select(c => new
                {
                    Name = c.Name,
                    Books = c.CategoryBooks.Select(b => new
                                {
                                    BookName = b.Book.Title,
                                    ReleaseDate = b.Book.ReleaseDate
                                }).OrderByDescending(b => b.ReleaseDate).Take(3).ToArray()
                }).ToArray();

            var builder = new StringBuilder();

            foreach (var cat in categories)
            {
                builder.AppendLine($"--{cat.Name}");
                foreach (var b in cat.Books)
                {
                    builder.AppendLine($"{b.BookName} ({b.ReleaseDate.Value.Year})");
                }
            }

            return builder.ToString();

        } //13.

        public static void IncreasePrices(BookShopContext db)
        {
            var books = db.Books.Where(b => b.ReleaseDate.Value.Year < 2010);

            foreach (var b in books)
            {
                b.Price += 5;
            }

            db.SaveChanges();
        } //14.

        public static int RemoveBooks(BookShopContext db)
        {
            var books = db.Books.Where(b => b.Copies < 4200);

            var firstBooksCount = db.Books.Count();

            db.Books.RemoveRange(books);

            db.SaveChanges();

            var secondBooksCount = db.Books.Count();

            var result = firstBooksCount - secondBooksCount;

            return result;
        } //15.
    }
}