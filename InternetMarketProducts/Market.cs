using System;
using System.Collections.Generic;
using System.Text;
using Wintellect.PowerCollections;

namespace MarketProducts
{
    public class Product : IComparable<Product>
    {
        private string name;
        private decimal price;
        private string producer;

        public Product(string name, string price, string producer)
        {
            this.name = name;
            this.price = decimal.Parse(price);
            this.producer = producer;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }

        public string Producer
        {
            get { return producer; }
            set { producer = value; }
        }

        public string GetNameAndProducerKey()
        {
            string key = name + ";" + producer;
            return key;
        }

        public int CompareTo(Product other)
        {
            int resultOfCompare = name.CompareTo(other.name);
            if (resultOfCompare == 0)
            {
                resultOfCompare = producer.CompareTo(other.producer);
            }
            if (resultOfCompare == 0)
            {
                resultOfCompare = price.CompareTo(other.price);
            }
            return resultOfCompare;
        }

        public override string ToString()
        {
            string toString = "{" + name + ";" + producer + ";" + price.ToString("0.00") + "}";
            return toString;
        }
    }

    class Market
    {
        private static string ComAddProduct = "Product added";
        private static string ComDeleteProducts = "products deleted";
        private static string ComNoProduct = "No products found";

        private static MultiDictionary<string, Product> productsByName;
        private static MultiDictionary<string, Product> nameAndProducer;
        private static OrderedMultiDictionary<decimal, Product> productsByPrice;
        private static MultiDictionary<string, Product> productsByProducer;
        private static StringBuilder generalSB;
        private static OrderedBag<Product> beforePrintBag = new OrderedBag<Product>();

        static void Main()
        {
//            //This section is compiled in Debug Mode only.
//#if DEBUG
//            Console.SetIn(new System.IO.StreamReader("input.txt"));
//#endif
            int length = int.Parse(Console.ReadLine());
            generalSB = new StringBuilder();
            ReadLoadInput(length);
            Console.WriteLine(generalSB);
        }

        private static void ReadLoadInput(int length)
        {
            productsByName = new MultiDictionary<string, Product>(true);
            nameAndProducer = new MultiDictionary<string, Product>(true);
            productsByPrice = new OrderedMultiDictionary<decimal, Product>(true);
            productsByProducer = new MultiDictionary<string, Product>(true);

            for (int i = 0; i < length; i++)
            {
                string currentToken = Console.ReadLine();
                FindCommand(currentToken[0], currentToken);
            }
        }

        private static void FindCommand(char keyChar, string currentToken)
        {
            switch (keyChar)
            {
                case 'A':
                    AddProductFunction(currentToken);
                    break;
                case 'D':
                    DeleteProductFunction(currentToken);
                    break;
                case 'F':
                    FindProductFunction(currentToken);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
                    break;
            }
        }

        #region Find products
        private static void FindProductFunction(string currentToken)
        {
            switch (currentToken[16])
            {
                case 'm': // name
                    string toUse = currentToken.Substring(19);
                    FindByName(toUse);
                    break;
                case 'o': // producer
                    toUse = currentToken.Substring(23);
                    FindByProducer(toUse);
                    break;
                case 'i': // price range
                    toUse = currentToken.Substring(25);
                    FindByPriceRange(toUse);
                    //TODO
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
                    break;
            }
        }

        private static void FindByPriceRange(string toUse)
        {
            int index = toUse.IndexOf(';');
            decimal startPrice = decimal.Parse(toUse.Substring(0,index));
            decimal endPrice = decimal.Parse(toUse.Substring(index+1));

            var matchCollection = productsByPrice.Range(startPrice, true, endPrice, true);
            if (matchCollection.Count == 0)
            {
                generalSB.AppendLine(ComNoProduct);
            }
            else
            {
                beforePrintBag.Clear();
                foreach (var product in matchCollection.Values)
                {
                    beforePrintBag.Add(product);
                }
                foreach (var product in beforePrintBag)
                {
                    string formatLine = product.ToString();
                    generalSB.AppendLine(formatLine);
                }
            }
        }

        private static void FindByProducer(string toUse)
        {
            if (productsByProducer.ContainsKey(toUse))
            {
                var matchCollection = productsByProducer[toUse];
                beforePrintBag.Clear();
                foreach (var product in matchCollection)
                {
                    beforePrintBag.Add(product);
                }
                foreach (var product in beforePrintBag)
                {
                    string formatLine = product.ToString();
                    generalSB.AppendLine(formatLine);
                }
            }
            else
            {
                generalSB.AppendLine(ComNoProduct);
            }
        }

        private static void FindByName(string toUse)
        {
            if (productsByName.ContainsKey(toUse))
            {
                var matchCollection = productsByName[toUse];
                beforePrintBag.Clear();
                foreach (var product in matchCollection)
                {
                    beforePrintBag.Add(product);
                }
                foreach (var product in beforePrintBag)
                {
                    string formatLine = product.ToString();
                    generalSB.AppendLine(formatLine);
                }
            }
            else
            {
                generalSB.AppendLine(ComNoProduct);
            }
        }
        #endregion


        #region Add product
        private static void AddProductFunction(string currentLine)
        {
            string toUse = currentLine.Substring(11);
            string[] itemsToAdd = toUse.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            string producer = itemsToAdd[2].Trim();
            string productName = itemsToAdd[0].Trim();
            string price = (itemsToAdd[1].Trim());

            Product product = new Product(productName, price, producer);
            productsByName.Add(productName, product);                       /// add by name
            string nameAndProducerKey = product.GetNameAndProducerKey();
            nameAndProducer.Add(nameAndProducerKey, product);               /// add by name and producer
            productsByPrice.Add(product.Price, product);                    /// add by price
            productsByProducer.Add(producer, product);                      /// add by producer
            generalSB.AppendLine(ComAddProduct);
        }
        #endregion Add product


        #region Delete products
        private static void DeleteProductFunction(string currentLine)
        {
            string toUse = currentLine.Substring(15);
            string[] itemsToAdd = toUse.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (itemsToAdd.Length == 1)
            {
                DeleteByProducer(itemsToAdd);
            }
            else
            {
                DeleteByNameAndProducer(itemsToAdd);
            }
        }

        private static void DeleteByProducer(string[] itemsToAdd)
        {
            string onlyProducerKey = itemsToAdd[0].Trim();

            if (!productsByProducer.ContainsKey(onlyProducerKey))
            {
                generalSB.AppendLine(ComNoProduct);
            }
            else
            {
                var productsToBeRemoved = productsByProducer[onlyProducerKey]; /// copy a collection with matches
                foreach (var product in productsToBeRemoved)
                {
                    string nameAndProducerKey = product.Name + ";" + onlyProducerKey;
                    productsByName.Remove(product.Name, product);
                    productsByPrice.Remove(product.Price, product);
                    nameAndProducer.Remove(nameAndProducerKey, product);
                }
                int countOfRemovedProducts = productsByProducer[onlyProducerKey].Count;
                productsByProducer.Remove(onlyProducerKey);
                generalSB.AppendFormat("{0} {1}\n", countOfRemovedProducts, ComDeleteProducts);
            }
        }

        private static void DeleteByNameAndProducer(string[] itemsToAdd)
        {
            string producer = itemsToAdd[1].Trim();
            string productName = itemsToAdd[0].Trim();

            string nameAndProducerKey = productName + ";" + producer;
            if (!nameAndProducer.ContainsKey(nameAndProducerKey))
            {
                generalSB.AppendLine(ComNoProduct);
            }
            else
            {
                var productsToBeRemoved = nameAndProducer[nameAndProducerKey]; /// copy a collection with matches
                foreach (var product in productsToBeRemoved)
                {
                    productsByName.Remove(productName, product);
                    productsByProducer.Remove(producer, product);
                    productsByPrice.Remove(product.Price, product);
                }
                int countOfRemovedProducts = nameAndProducer[nameAndProducerKey].Count;
                nameAndProducer.Remove(nameAndProducerKey);
                generalSB.AppendFormat("{0} {1}\n", countOfRemovedProducts, ComDeleteProducts);
            }
        }
        #endregion Delete products
    }
}

