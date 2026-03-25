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
        while (true) //lopp that is always true, unless user inputs "4"
        {
            Console.WriteLine("Inventory Manager");
            Console.WriteLine("1. Add an item");
            Console.WriteLine("2. Update or Remove an existing item");
            Console.WriteLine("3. View all items");
            Console.WriteLine("4. Exit program");
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
                    Console.WriteLine($"Total inventory value: £{CalculateInventoryValue():0.00}");
                    break;
                case "4":
                    Environment.Exit(0); // The only real way to exit the app
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
        string productName = (Console.ReadLine() ?? "").Trim(); //variable that will hold the name, ignoring spaces. for this method.
        itemName.Add(productName);
        Console.WriteLine("Enter the quantity for '" + productName + "':");
        double productQuantity; //Variable for quantity for this method
        while (!double.TryParse(Console.ReadLine(), out productQuantity))
        {
            Console.WriteLine("Invalid number, please enter quantity: ");
        }
        itemQuantity.Add(productQuantity);
        Console.WriteLine("Enter unit price for '" + productName + "':");
        double productPrice; //variable that hold the price for this method.
        while (!double.TryParse(Console.ReadLine(), out productPrice)) // incase the input is wrong, it will prompt the user again.
        {
            Console.WriteLine("Invalid number, please enter price again: ");
        }
        itemPrice.Add(productPrice);
        Console.WriteLine($"Item '{productName}' has been added to the inventory.");
    }

    static void UpdateItem()
    {
        ViewItem(); //printing all items before choosing option. for easier selection
        if (itemName.Count == 0)
        {
            Console.WriteLine("Inventory is empty, please add an item!");
            return;
        }
        Console.WriteLine(
            "Please enter existing item number to continue OR anything else to go back to main menu."
        );
        if (
            int.TryParse(Console.ReadLine(), out int productNumber)
            && productNumber > 0
            && productNumber <= itemName.Count
        )
        {
            Console.WriteLine("Choose an option for: " + itemName[productNumber - 1]);
            Console.WriteLine("1. Add more to stock");
            Console.WriteLine("2. Reduce from stock");
            Console.WriteLine("3. Delete the item from inventory");
            Console.WriteLine("4. Go back");
            int updateChoice;
            while (
                !int.TryParse(Console.ReadLine(), out updateChoice)
                || updateChoice < 1
                || updateChoice > 4
            )
            {
                Console.WriteLine("Invalid selection");
                Console.WriteLine("1. Add more to stock");
                Console.WriteLine("2. Reduce from stock");
                Console.WriteLine("3. Delete the item from inventory");
                Console.WriteLine("4. Go back");
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
                        "Updated quantity for '"
                            + itemName[productNumber - 1]
                            + "' : "
                            + itemQuantity[productNumber - 1]
                            + "x"
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
                        "Updated quantity for '"
                            + itemName[productNumber - 1]
                            + "': "
                            + itemQuantity[productNumber - 1]
                            + "x"
                    );
                    break;

                case 3:
                    Console.WriteLine(
                        "Are you sure you want to remove: '"
                            + itemName[productNumber - 1]
                            + "' from the inventory?"
                            + " Type 'yes' or 'no' "
                    );
                    string? input = Console.ReadLine()?.Trim().ToLower();

                    if (input == "yes")
                    {
                        string removedItem = itemName[productNumber - 1];
                        itemName.RemoveAt(productNumber - 1);
                        itemQuantity.RemoveAt(productNumber - 1);
                        itemPrice.RemoveAt(productNumber - 1);
                        Console.WriteLine(
                            $"Item {productNumber}. '{removedItem}'is removed sucessfuly!"
                        );
                    }
                    else if (input == "no")
                    {
                        Console.WriteLine(
                            $"Item '{itemName[productNumber - 1]}' was NOT removed from the inventory!"
                        );
                    }
                    else
                    {
                        Console.WriteLine("Invalid selection, returned to main menu");
                    }
                    break;
                case 4:
                    return;

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
                $"{displayNumber}. {itemName[i]} - Quantity: {itemQuantity[i]}x - Unit price: £{itemPrice[i]:0.00}  // Total Value: £{totalValue:0.00}"
            );
        }
    }

    static double CalculateInventoryValue()
    {
        double totalValue = 0;
        for (int i = 0; i < itemName.Count; i++)
        {
            totalValue += itemQuantity[i] * itemPrice[i];
        }
        return totalValue;
    }
}
