using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_DZ_OOP_9
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Product> products = new List<Product>() { new Product("Макароны", 150), new Product("Газировка", 200), new Product("Масло", 300) };
            List<Clients> clients = new List<Clients>() { new Clients(1000), new Clients(300), new Clients(100) };
            Magazine magazine = new Magazine();
            magazine.Work(products, clients);
        }
    }

    class Magazine
    {
        public void Work(List<Product> products, List<Clients> clients)
        {
            int clientNumber = 1;

            while (clients.Count > 0)
            {

                List<Product> buyingProducts = new List<Product>();

                foreach (var client in clients)
                {
                    client.Buy(products, buyingProducts);

                    if(buyingProducts.Count > 0)
                    {
                        Console.WriteLine("Клиент - " + clientNumber + " купил");

                        foreach (var product in buyingProducts)
                        {
                            Console.WriteLine(product.Name);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Мне не хватило денег на продукты!");
                    }
                    clients.Remove(client);
                    Console.ReadKey();
                    break;
                }
                clientNumber++;
            }
            Console.WriteLine("Вроде это был последний!");
        }
    }

    class Product
    {
        public string Name { get; private set; }

        public int Cost { get; private set; }

        public Product(string name, int cost)
        {
            Name = name;
            Cost = cost;
        }
    }

    class Clients
    {
        public int Money { get; private set; }

        public Clients(int money)
        {
            Money = money;
        }

        public void Buy(List<Product> products, List<Product> buyingProducts)
        {
            Random random = new Random();

            var productInCart = products.OrderBy(x => random.Next()).ToList();

            while(productInCart.Count > 0)
            {
                foreach (var product in productInCart)
                {
                    if(product.Cost <= Money)
                    {
                        Money -= product.Cost;
                        buyingProducts.Add(product);
                        productInCart.Remove(product);
                    }
                    else
                    {
                        Console.WriteLine("Я не смог купить - " + product.Name);
                        productInCart.Remove(product);
                    }
                    break;
                }
            }
        }
    }
}
