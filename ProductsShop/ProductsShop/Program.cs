namespace ProductsShop.App
{
    using System;
    using ProductsShop.Data;
    using Newtonsoft.Json;
    using ProductsShop.Models;
    using System.IO;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using System.Xml.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            ResetDatabase();
            JsonQueries();
            XmlQueries();
        }
        //--xmlQueries
        private static void XmlQueries()
        {
            GetProductsInRangeXml();
            GetSuccessfullySoldProductsXml();
            GetCategoriesByProductsCountXml();
            GetUsersAndProductsXml();
        }

        static void GetUsersAndProductsXml()
        {
            using (var db = new ProductsShopContext())
            {
                var users = db.Users
                .Where(u => u.soldProducts.Count > 0)
                .Include(u => u.soldProducts)
                .OrderByDescending(u => u.soldProducts.Count)
                .ThenBy(u => u.LastName)
                .Select(u => new
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    age = u.Age,
                    soldProducts = new
                    {
                        count = u.soldProducts.Count,
                        products = u.soldProducts.Select(p => new
                        {
                            name = p.Name,
                            price = p.Price
                        })
                    }
                });
                
                var usersToSerialize = new
                {
                    usersCount = users.Count(),
                    users
                };

                var xmlDoc = new XDocument(new XDeclaration("1.0", "utf-8", null), new XElement("users", new XAttribute("count", usersToSerialize.usersCount)));

                foreach (var u in usersToSerialize.users)
                {
                    var user = new XElement("user");
                    if (u.firstName != null)
                    {
                        user.Add(new XAttribute("first-name", u.firstName));
                    }
                    user.Add(new XAttribute("last-name", u.lastName));
                    if (u.age != null)
                    {
                        user.Add(new XAttribute("age", u.age));
                    }

                    var soldProducts = new XElement("sold-products", new XAttribute("count", u.soldProducts.count));

                    foreach (var p in u.soldProducts.products)
                    {
                        var product = new XElement("product",
                            new XAttribute("name", p.name),
                            new XAttribute("price", p.price));
                        soldProducts.Add(product);
                    }

                    user.Add(soldProducts);
                    xmlDoc.Root.Add(user);
                }
                File.WriteAllText("UsersAndProducts.xml", xmlDoc.ToString());
            }
        }

        static void GetCategoriesByProductsCountXml()
        {
            using (var db = new ProductsShopContext())
            {
                var categories = db.Categories
                    .Include(c => c.Products)
                    .OrderBy(c => c.Name)
                    .Select(c => new
                    {
                        category = c.Name,
                        productsCount = c.Products.Count,
                        averagePrice = $"{c.Products.Sum(p => p.Product.Price) / c.Products.Count:f2}",
                        totalRevenue = $"{c.Products.Sum(p => p.Product.Price):f2}"
                    }).ToArray();

                var xmlDoc = new XDocument(new XDeclaration("1.0", "utf-8", null), new XElement("categories"));

                foreach (var c in categories)
                {
                    var category = new XElement("category", new XAttribute("name", c.category));
                    category.Add(new XElement("product-count", c.productsCount), 
                                 new XElement("average-price", c.averagePrice), 
                                 new XElement("total-revenue", c.totalRevenue));
                    xmlDoc.Root.Add(category);
                }
                File.WriteAllText("CategoriesByProductsCount.xml", xmlDoc.ToString());
            }
        }

        static void GetSuccessfullySoldProductsXml()
        {
            using (var db = new ProductsShopContext())
            {
                var allSoldProducts = db
                .Products
                .Where(p => p.BuyerId != null)
                .Include(p => p.Seller)
                .Include(p => p.Buyer)
                .ToArray();

                var selectedSellers = allSoldProducts
                    .Select(p => new
                    {
                        firstName = p.Seller.FirstName,
                        lastName = p.Seller.LastName,
                        soldProducts = p.Seller
                            .soldProducts
                            .Select(ps => new
                            {
                                name = p.Name,
                                price = p.Price,
                                buyerFirstName = p.Buyer.FirstName,
                                buyerLastName = p.Buyer.LastName
                            })
                            .ToArray()
                    })
                    .OrderBy(a => a.lastName)
                    .ThenBy(a => a.firstName)
                    .ToArray();

                var xmlDoc = new XDocument(new XDeclaration("1.0", "utf-8", null), new XElement("users"));

                foreach (var seller in selectedSellers)
                {
                    var user = new XElement("user");

                    if (seller.firstName != null)
                    {
                        user.Add(new XAttribute("first-name", seller.firstName));
                    }

                    user.Add(new XAttribute("last-name", seller.lastName));

                    var soldProducts = new List<XElement>();

                    foreach (var product in seller.soldProducts)
                    {
                        var childOfSoldProducts = new XElement("product",
                                            new XElement("name", product.name),
                                            new XElement("price", product.price));

                        soldProducts.Add(childOfSoldProducts);
                    }

                    user.Add(new XElement("sold-products", soldProducts));

                    xmlDoc.Root.Add(user);
                }
                File.WriteAllText("SuccessfullySoldProducts.xml", xmlDoc.ToString());
            }
        }

        static void GetProductsInRangeXml()
        {
            using (var db = new ProductsShopContext())
            {
                var products = db.Products
                    .Include(p => p.Seller)
                    .Where(p => p.Price >= 500 && p.Price <= 1000).Select(p => new
                    {
                        Name = p.Name,
                        Price = p.Price,
                        Seller = $"{p.Seller.FirstName} {p.Seller.LastName}"
                    })
                    .OrderBy(p => p.Price)
                    .ToArray();

                var xDoc = new XDocument();
                xDoc.Add(new XElement("products"));

                foreach (var p in products)
                {
                    xDoc.Root.Add(new XElement("product", new XAttribute("name", p.Name), new XAttribute("price", $"{p.Price}"), new XAttribute("buyer", p.Seller)));
                }

                File.WriteAllText("ProductsInRange(500-1000).xml", xDoc.ToString());
            }
        }

        //--jsonQueries
        static void JsonQueries()
        {
            GetProductsInRangeJson();
            GetSuccessfullySoldProductsJson();
            GetCategoriesByProductsCountJson();
            GetUsersAndProductsJson();
        }

        static void GetUsersAndProductsJson()
        {
            using (var db = new ProductsShopContext())
            {
                var users = db.Users
                .Where(u => u.soldProducts.Count > 0)
                .Include(u => u.soldProducts)
                .OrderByDescending(u => u.soldProducts.Count)
                .ThenBy(u => u.LastName)
                .Select(u => new
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    age = u.Age,
                    soldProducts = new
                    {
                        count = u.soldProducts.Count,
                        products = u.soldProducts.Select(p => new
                        {
                            name = p.Name,
                            price = p.Price
                        })
                    }
                });

                var path = "UsersAndProducts.json";

                var usersToSerialize = new
                {
                    usersCount = users.Count(),
                    users
                };

                var json = JsonConvert.SerializeObject(usersToSerialize, Formatting.Indented, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });
                
                File.WriteAllText("UsersAndProducts.json", json);
            }
        }

        static void GetCategoriesByProductsCountJson()
        {
            using (var db = new ProductsShopContext())
            {
                var categories = db.Categories
                    .Include(c => c.Products)
                    .OrderBy(c => c.Name)
                    .Select(c => new
                    {
                        category = c.Name,
                        productsCount = c.Products.Count,
                        averagePrice = $"{c.Products.Sum(p => p.Product.Price) / c.Products.Count:f2}",
                        totalRevenue = $"{c.Products.Sum(p => p.Product.Price):f2}"
                    }).ToArray();

                var json = JsonConvert.SerializeObject(categories, Formatting.Indented, new JsonSerializerSettings
                {
                    DefaultValueHandling = DefaultValueHandling.Ignore
                });

                File.WriteAllText("CategoriesByProductsCount.json", json);
            }
        }

        static void GetSuccessfullySoldProductsJson()
        {
            using (var db = new ProductsShopContext())
            {
                var sellers = db.Users
                    .Include(s => s.soldProducts)
                    .Where(s => s.soldProducts.Count > 0)
                    .OrderBy(s => s.LastName)
                    .ThenBy(s => s.LastName)
                    .Select(s => new
                    {
                        firstName = s.FirstName,
                        lastName = s.LastName,
                        soldProducts = s.soldProducts.Select(sp => new
                        {
                            name = sp.Name,
                            price = sp.Price,
                            buyerFirstName = sp.Buyer.FirstName,
                            buyerLastName = sp.Buyer.LastName,
                        })
                    }).ToArray();

                var json = JsonConvert.SerializeObject(sellers, Formatting.Indented);

                File.WriteAllText("SuccessfullySoldProducts.json", json);
            }
        }

        static void GetProductsInRangeJson()
        {
            using (var db = new ProductsShopContext())
            {
                var products = db.Products
                    .Include(p => p.Seller)
                    .Where(p => p.Price >= 500 && p.Price <= 1000).Select(p => new
                    {
                        Name = p.Name,
                        Price = p.Price,
                        Seller = $"{p.Seller.FirstName} {p.Seller.LastName}"
                    })
                    .OrderBy(p => p.Price)
                    .ToArray();

                var json = JsonConvert.SerializeObject(products, Formatting.Indented);

                File.WriteAllText("ProductsInRange(500-1000).json", json);
            }
        }

        //ResetDb(pick XML or JSON)!
        private static void ResetDatabase()
        {
            using (var db = new ProductsShopContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                //JsonImport();
                //XmlImport();
            }
        }

        //----XML-Import
        private static void XmlImport()
        {
            var importUsers = ImportUsersFromXml();
            Console.WriteLine(importUsers);
            var importCategories = ImportCategoriesXml();
            Console.WriteLine(importCategories);
            var importProducts = ImportProductsXml();
            Console.WriteLine(importProducts);
        }

        static string ImportProductsXml()
        {
            var path = "products.xml";

            string xmlString = File.ReadAllText(path);

            var xmlDoc = XDocument.Parse(xmlString);

            var elements = xmlDoc.Root.Elements();

            using (var db = new ProductsShopContext())
            {
                var categoryProducts = new List<CategoryProducts>();

                var userIds = db.Users.Select(u => u.UserId).OrderBy(u => u).ToArray();
                var categoryIds = db.Categories.Select(c => c.CategoryId).OrderBy(c => c).ToArray();

                var rnd = new Random();

                foreach (var e in elements)
                {
                    int userIndex = rnd.Next(0, userIds.Length);
                    int sellerId = userIds[userIndex];
                    int categoryIndex = rnd.Next(0, categoryIds.Length);
                    int categoryId = categoryIds[categoryIndex];

                    var product = new Product()
                    {
                        Name = e.Element("name").Value,
                        Price = decimal.Parse(e.Element("price").Value),
                        SellerId = sellerId
                    };

                    var buyerId = rnd.Next(0, userIds.Length);

                    if (product.BuyerId != userIds[buyerId])
                    {
                        product.BuyerId = userIds[buyerId];
                    }

                    if (product.BuyerId - product.SellerId < 5 && product.BuyerId - product.SellerId > 0)
                    {
                        product.BuyerId = null;
                    }

                    var catProduct = new CategoryProducts()
                    {
                        Product = product,
                        CategoryId = categoryId
                    };

                    categoryProducts.Add(catProduct);
                }

                db.AddRange(categoryProducts);
                db.SaveChanges();
                return $"{categoryProducts.Count} categories and products were imported from file: {path}";
            }
        }

        static string ImportCategoriesXml()
        {
            var path = "categories.xml";

            string xmlString = File.ReadAllText(path);

            var xmlDoc = XDocument.Parse(xmlString);

            var elements = xmlDoc.Root.Elements();

            var categories = new List<Category>();

            foreach (var e in elements)
            {
                var category = new Category()
                {
                    Name = e.Element("name").Value
                };

                categories.Add(category);
            }

            using (var db = new ProductsShopContext())
            {
                db.Categories.AddRange(categories);
                db.SaveChanges();
                return $"{categories.Count} categories were imported from file: {path}";
            }
        }

        static string ImportUsersFromXml()
        {
            var path = "users.xml";

            string xmlString = File.ReadAllText(path);

            var xmlDoc = XDocument.Parse(xmlString);

            var users = new List<User>();

            var elements = xmlDoc.Root.Elements();

            foreach (var e in elements)
            {
                string firstName = e.Attribute("firstName")?.Value;
                string lastName = e.Attribute("lastName").Value;

                int? age = null;

                if (e.Attribute("age") != null)
                {
                    age = int.Parse(e.Attribute("age").Value);
                }
                
                var user = new User()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Age = age
                };
                users.Add(user);
            }

            using (var db = new ProductsShopContext())
            {
                db.Users.AddRange(users);
                db.SaveChanges();
                return $"{users.Count} users were imported from file: {path}";
            }
        }

        //----JSON-Import
        private static void JsonImport()
        {
            var impUsers = ImportUsersFromJson();
            Console.WriteLine(impUsers);
            var impCategories = ImportCategoriesFromJson();
            Console.WriteLine(impCategories);
            var impProducts = ImportProductsFromJson();
            Console.WriteLine(impProducts);
            SetCategories();
        }

        static void SetCategories()
        {
            using (var db = new ProductsShopContext())
            {
                var random = new Random();

                var productIds = db.Products.AsNoTracking().Select(p => p.ProductId).OrderBy(p => p).ToArray();
                var categoryIds = db.Categories.AsNoTracking().Select(c => c.CategoryId).ToArray();
                int categoryCount = categoryIds.Length;

                var categoryProducts = new List<CategoryProducts>();

                foreach (var p in productIds)
                {
                    for (int i = 0; i < random.Next(0, categoryCount); i++)
                    {
                        int index = random.Next(0, categoryCount);
                        while (categoryProducts.Any(cp => cp.ProductId == p && cp.CategoryId == categoryIds[index]))
                        {
                            index = random.Next(0, categoryCount);
                        }

                        var catPr = new CategoryProducts()
                        {
                            ProductId = p,
                            CategoryId = categoryIds[index]
                        };
                        categoryProducts.Add(catPr);
                    }
                }

                db.CategoriesProducts.AddRange(categoryProducts);
                db.SaveChanges();
            }
        }

        static string ImportProductsFromJson()
        {
            string path = "products.json";

            Product[] products = ImportJson<Product>(path);

            Random random = new Random();

            using (var db = new ProductsShopContext())
            {
                int[] usersIds = db.Users.Select(u => u.UserId).ToArray();

                foreach (var p in products)
                {
                    int sellerIndex = random.Next(0, usersIds.Length);
                    int sellerId = usersIds[sellerIndex];

                    int? buyerId = sellerId;
                    while (buyerId == sellerId)
                    {
                        int buyerIndex = random.Next(0, usersIds.Length);
                        buyerId = usersIds[buyerIndex];
                    }

                    if (buyerId - sellerId < 5 && buyerId - sellerId > 0)
                    {
                        buyerId = null;
                    }

                    p.SellerId = sellerId;
                    p.BuyerId = buyerId;
                    db.Products.Add(p);
                }
                
                db.SaveChanges();
                string result = $"{products.Length} categories were imported from file: {path}";
                return result;
            }
        }

        static string ImportCategoriesFromJson()
        {
            var path = "categories.json";

            Category[] categories = ImportJson<Category>(path);

            using (var db = new ProductsShopContext())
            {
                db.Categories.AddRange(categories);
                db.SaveChanges();
                string result = $"{categories.Length} categories were imported from file: {path}";
                return result;
            }
        }

        static string ImportUsersFromJson()
        {
            var path = "users.json";

            User[] users = ImportJson<User>(path);

            using (var db = new ProductsShopContext())
            {
                db.Users.AddRange(users);
                db.SaveChanges();
                string result = $"{users.Length} users were imported from file: {path}";
                return result;
            }
        }

        static T[] ImportJson<T>(string path)
        {
            var jsonString = File.ReadAllText(path);
            T[] objects = JsonConvert.DeserializeObject<T[]>(jsonString);

            return objects;
        }
    }
}
