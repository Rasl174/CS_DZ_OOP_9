using System;
using System.Collections.Generic;

namespace CS_DZ_OOP_9
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Clients> clients = new List<Clients>() { new Clients(1000), new Clients(300), new Clients(100) };
            Magazine magazine = new Magazine();
            magazine.Work(clients);
        }
    }

    class Magazine
    {
        public void Work(List<Clients> clients)
        {
            List<Product> products = new List<Product>() { new Product("Макароны", 150), new Product("Газировка", 200), new Product("Масло", 300) };

            int clientNumber = 1;

            while (clients.Count > 0)
            {
                List<Product> clientCart = new List<Product>();
                List<Product> buyingProducts = new List<Product>();

                foreach (var client in clients)
                {
                    foreach (var product in products)
                    {
                        clientCart.Add(product);
                    }

                    client.Buy(clientCart, buyingProducts);

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

        public void Buy(List<Product> productCart, List<Product> buyingProducts)
        {
            int allCostProducts = 0;

            Random random = new Random();

            for (int i = productCart.Count - 1; i >= 1; i--)
            {
                int j = random.Next(i + 1);

                Product productInCart = productCart[j];
                productCart[j] = productCart[i];
                productCart[i] = productInCart;
            }

            foreach (var product in productCart)
            {
                allCostProducts += product.Cost;
            }

            if(Money >= allCostProducts)
            {
                foreach (var product in productCart)
                {
                    buyingProducts.Add(product);
                }
                Console.WriteLine("У меня на все хватило денег!");
            }
            else
            {
                while (productCart.Count > 0)
                {
                    foreach (var product in productCart)
                    {
                        if(product.Cost <= Money)
                        {
                            Money -= product.Cost;
                            buyingProducts.Add(product);
                            productCart.Remove(product);
                        }
                        else
                        {
                            Console.WriteLine("Мне не хватает на - " + product.Name);
                            productCart.Remove(product);
                        }
                        break;
                    }
                }
            }
        }
    }
}
