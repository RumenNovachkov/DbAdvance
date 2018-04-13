using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace ExternalFormatProcessing
{
    class Program
    {
        static void Main(string[] args)
        {


            //var product = new Product()
            //{
            //    Name = "Jam",
            //    Description = "Sushtoto ama na turski",
            //    ManufacturerId = 0
            //};

            //var obj = new
            //{
            //    Name = "Ilarion",
            //    Age = 200,
            //    Books = new []
            //    {
            //        "Istoriq Slavqnobulgarska",
            //        "Do Chikago i nazad",
            //        "Otneseni ot Demokraciqta"
            //    }
            //};

            //var jsonString = JsonConvert.SerializeObject(obj);
            //File.WriteAllText("newJson.json" ,jsonString);
            //var jsonString = JsonConvert.SerializeObject(product, Formatting.Indented, new JsonSerializerSettings()
            //{
            //    DefaultValueHandling = DefaultValueHandling.Ignore
            //});

            //var parsedJson = JsonConvert.DeserializeObject(jsonString);

            //var template = new
            //{
            //    Name = default(string),
            //    Age = default(int),
            //    Books = new List<string>
            //    {
            //    }
            //};

            //var jsonFormatFile = File.ReadAllText("newJson.json");

            //var parcedProdict = JsonConvert.DeserializeAnonymousType(jsonFormatFile, template);

            //Console.WriteLine(string.Join(Environment.NewLine, parcedProdict.Books));
        }

        //private static T DeserializeObject<T>(string jsonString)
        //{
        //    var jsonBytes = Encoding.UTF8.GetBytes(jsonString);
        //
        //    using (var stream = new MemoryStream(jsonBytes))
        //    {
        //        var serializer = new DataContractJsonSerializer(typeof(T));
        //        var obj = (T)serializer.ReadObject(stream);
        //        return obj;
        //    }
        //}

        //private static string SerializeObject(Product product)
        //{
        //    var jsonSerializer = new DataContractJsonSerializer(product.GetType());
        //
        //    using (var stream = new MemoryStream())
        //    {
        //        jsonSerializer.WriteObject(stream, product);
        //        var result = Encoding.UTF8.GetString(stream.ToArray());
        //
        //        return result;
        //    }
        //}
    }
}
