using System;
using System.Collections.Generic;

namespace CS_DZ_OOP_9
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Client> clients = new List<Client>() { new Client(1000), new Client(500), new Client(100) };
            Store store = new Store();
            store.Work(clients);
        }
    }

    class Store
    {
        private List<Product> _products = new List<Product>() { new Product("Макароны", 150), new Product("Газировка", 200), new Product("Масло", 300) };

        public void Work(List<Client> clients)
        {

            int clientNumber = 1;

            while (clients.Count > 0)
            {
                Console.WriteLine("Клиент - " + clientNumber);
                foreach (var client in clients)
                {
                    client.Buy(_products);
                    clients.Remove(client);
                    break;
                }
                Console.ReadKey();
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

    class Client
    {
        public int Money { get; private set; }

        public Client(int money)
        {
            Money = money;
        }

        public void Buy(List<Product> products)
        {
            List<Product> buyingProducts = new List<Product>();
            List<Product> cart = new List<Product>();

            foreach (var product in products)
            {
                cart.Add(product);
            }
            int allCostProducts = 0;

            SumProductCost(cart, ref allCostProducts);

            if(Money >= allCostProducts)
            {
                foreach (var product in cart)
                {
                    buyingProducts.Add(product);
                    Pay(product.Cost);
                }
                Console.WriteLine("У меня на все хватило денег!");
            }
            else
            {
                ShuffleCartProducts(cart);
                while (cart.Count > 0)
                {
                    foreach (var product in cart)
                    {
                        if(product.Cost <= Money)
                        {
                            buyingProducts.Add(product);
                            Pay(product.Cost);
                            cart.Remove(product);
                        }
                        else
                        {
                            Console.WriteLine("Мне не хватает на - " + product.Name);
                            cart.Remove(product);
                        }
                        break;
                    }
                }
            }
            ShowBuyingProducts(buyingProducts);
        }

        public int SumProductCost(List<Product> cart, ref int allCostProducts)
        {
            foreach (var product in cart)
            {
                allCostProducts += product.Cost;
            }
            return allCostProducts;
        }

        public void ShuffleCartProducts(List<Product> products)
        {
            Random random = new Random();

            for (int i = products.Count - 1; i >= 1; i--)
            {
                int inex = random.Next(i + 1);

                Product productInCart = products[inex];
                products[inex] = products[i];
                products[i] = productInCart;
            }
        }

        public void ShowBuyingProducts(List<Product> products)
        {
            if (products.Count > 0)
            {
                Console.WriteLine("Я купил:");
                foreach (var product in products)
                {
                    Console.WriteLine(product.Name);
                }
            }
            else
            {
                Console.WriteLine("Мне ни на что не хватило!");
            }
        }

        public void Pay(int cost)
        {
            Money -= cost;
        }
    }
}
