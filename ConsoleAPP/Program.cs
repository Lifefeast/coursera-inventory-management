using System;
using System.ComponentModel.DataAnnotations;
using System.IO.Compression;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;

/* Inventory management system

To complete this challenge, you will need to create a console application where users can manage product stock. Users should be able to add new products, update stock, and remove products.

Some key features include:

Add new products with name, price, and stock quantity.

Update stock when products are sold or restocked.

View all products and their stock levels.

Remove products from inventory. */
public class InventoryManager
{
    static List<string> itemName = new List<string>();
    static List<double> itemQuantity = new List<double>();
    static List<double> itemPrice = new List<double>();

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Inventory Manager");
            Console.WriteLine("1. Add item");
            Console.WriteLine("2. Update existing item");
            Console.WriteLine("3. View all items");
            Console.WriteLine("4. Exit");
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddItem();
                    break;
                case "2":
                    UpdateItem();
                    break;
                case "3":
                    ViewItem();
                    break;
                case "4":
                    Environment.Exit(0);
                    return;
                default:
                    Console.WriteLine("Invalid Choice");
                    break;
            }
        }
    }

    static void AddItem()
    {
        Console.WriteLine("Enter the Item name:");
        string productName = Console.ReadLine() ?? ""; //Creating the variable that will hold the name
        itemName.Add(productName);
        Console.WriteLine("Enter the quantity for " + productName + ":");
        double productQuantity;
        while (!double.TryParse(Console.ReadLine(), out productQuantity))
        {
            Console.WriteLine("Invalid number, please enter quantity: ");
        }
        itemQuantity.Add(productQuantity);
        Console.WriteLine("Enter unit price " + productName + ": ");
        double productPrice;
        while (!double.TryParse(Console.ReadLine(), out productPrice))
        {
            Console.WriteLine("Invalid number, please enter price again: ");
        }
        itemPrice.Add(productPrice);
    }

    static void UpdateItem()
    {
        if (itemName.Count == 0)
        {
            Console.WriteLine("Inventory is empty, please add an item!");
            return;
        }
        Console.WriteLine("Please enter the item number to continue: ");
        if (
            int.TryParse(Console.ReadLine(), out int productNumber)
            && productNumber > 0
            && productNumber <= itemName.Count
        )
        {
            Console.WriteLine("Did you buy or sell: " + itemName[productNumber - 1]);
            Console.WriteLine("1. Bought more");
            Console.WriteLine("2. Sold");
            int updateChoice;
            while (
                !int.TryParse(Console.ReadLine(), out updateChoice)
                || updateChoice < 1
                || updateChoice > 2
            )
            {
                Console.WriteLine("Invalid selection");
                Console.WriteLine("1. Bought more");
                Console.WriteLine("2. Sold");
            }
            switch (updateChoice)
            {
                case 1:
                    Console.WriteLine("How much " + itemName[productNumber - 1] + " did you buy?");
                    double boughtItem;
                    while (!double.TryParse(Console.ReadLine(), out boughtItem) || boughtItem <= 0)
                    {
                        Console.WriteLine("Invalid ammount, please enter again");
                    }
                    itemQuantity[productNumber - 1] += boughtItem;
                    Console.WriteLine(
                        "Updated quantity for ("
                            + itemName[productNumber - 1]
                            + "): "
                            + itemQuantity[productNumber - 1]
                    );
                    break;
                case 2:
                    Console.WriteLine("How much " + itemName[productNumber - 1] + " did you sell?");
                    double soldItem;
                    double currentStock = itemQuantity[productNumber - 1];
                    while (
                        !double.TryParse(Console.ReadLine(), out soldItem)
                        || soldItem < 0
                        || soldItem > itemQuantity[productNumber - 1]
                    )
                    {
                        Console.WriteLine(
                            "Invalid ammount, You have: "
                                + itemQuantity[productNumber - 1]
                                + "x of "
                                + itemName[productNumber - 1]
                        );
                    }
                    itemQuantity[productNumber - 1] -= soldItem;
                    Console.WriteLine(
                        "Updated quantity for ("
                            + itemName[productNumber - 1]
                            + "): "
                            + itemQuantity[productNumber - 1]
                    );
                    break;

                default:
                    Console.WriteLine("invalid Selection");
                    break;
            }
        }
    }

    static void ViewItem()
    {
        if (itemName.Count == 0)
        {
            Console.WriteLine("No Items available.");
            return;
        }
        Console.WriteLine("All items:");
        for (int i = 0; i < itemName.Count; i++)
        {
            int displayNumber = i + 1;
            double totalValue = itemQuantity[i] * itemPrice[i];
            Console.WriteLine(
                $"{displayNumber}. {itemName[i]} - {itemQuantity[i]}x - Unit price: £{itemPrice[i]:0.00}  // Total Price: £{totalValue:0.00}"
            );
        }
    }
}
