using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace TaskManager
{
    class Program
    {
        static async Task Main(string[] args)
        {
            TaskManager taskManager = new TaskManager();
            await taskManager.LoadTasksAsync();

            while (true)
            {
                Console.WriteLine("\nTask Manager Menu:");
                Console.WriteLine("1. Add Task");
                Console.WriteLine("2. View Tasks");
                Console.WriteLine("3. Update Task");
                Console.WriteLine("4. Delete Task");
                Console.WriteLine("5. Search Tasks");
                Console.WriteLine("6. View Progress");
                Console.WriteLine("7. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();

                Console.WriteLine("***********************************************");

                switch (choice)
                {
                    case "1":
                        taskManager.AddTask();
                        break;
                    case "2":
                        taskManager.ViewTasks();
                        break;
                    case "3":
                        taskManager.UpdateTask();
                        break;
                    case "4":
                        taskManager.DeleteTask();
                        break;
                    case "5":
                        taskManager.SearchTasks();
                        break;
                    case "6":
                        taskManager.ViewProgress();
                        break;
                    case "7":
                        await taskManager.SaveTasksAsync();
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
    } 
}
