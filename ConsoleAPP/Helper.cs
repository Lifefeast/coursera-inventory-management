using System;

public static class Helper // static class because all methods inside are static
{
    // Reusable yes/no method
    public static bool GetYesOrNo()
    {
        string? input = Console.ReadLine();
        if (input == null)
            return false; // default to "no" if nothing is entered

        input = input.Trim().ToLower();
        return input == "yes";
    }
}
/*    public static void CalculateTotal() { }
}

        while (true)
        {
            Console.WriteLine("Task Manager");
            Console.WriteLine("1. Add Task");
            Console.WriteLine("2. Mark Task as completed");
            Console.WriteLine("3. View Tasks");
            Console.WriteLine("4. Exit");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddTask();
                    break;
                case "2":
                    CompleteTask();
                    break;
                case "3":
                    ViewTask();
                    break;
                case "4":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid Choice");
                    break;
            }
        }
    }

    static void AddTask()
    {
        Console.WriteLine("Enter Task Description: ");
        string task = Console.ReadLine();
        tasks.Add(task);
        taskStatus.Add(false);
        Console.WriteLine("Task Added Sucessfully!");
    }

    static void CompleteTask()
    {
        if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks available to complete.");
            return;
        }
        Console.WriteLine("Enter task number to mark as completed: ");
        if (
            int.TryParse(Console.ReadLine(), out int taskNumber)
            && taskNumber > 0
            && taskNumber <= tasks.Count
        )
        {
            taskStatus[taskNumber - 1] = true;
            Console.WriteLine($"Task '{tasks[taskNumber - 1]}' marked as completed");
        }
        else
        {
            Console.WriteLine("Invalid Task Number");
        }
    }

    static void ViewTask()
    {
        if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks available.");
            return;
        }
        Console.WriteLine("Tasks:");
        for (int i = 0; i < tasks.Count; i++)
        {
            string status = taskStatus[i] ? "Completed" : "Pending";
            Console.WriteLine($"{i + 1}. {tasks[i]} - {status}");
        }
    }
}
*/
