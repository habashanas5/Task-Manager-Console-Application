using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TaskManager
{
    public class TaskManager
    {
        private readonly List<UserTask> tasks = new();
        private const string FilePath = "tasks.json";

        public void AddTask()
        {
            UserTask task = new UserTask();

            Console.Write("Enter Title: ");
            task.Title = Console.ReadLine();

            Console.Write("Enter Description: ");
            task.Description = Console.ReadLine();

            Console.Write("Enter Due Date (yyyy-MM-dd): ");
            task.DueDate = GetValidDate();

            Console.Write("Enter Priority (Low, Medium, High): ");
            task.Priority = GetValidInput(new[] { "Low", "Medium", "High" });

            Console.Write("Enter Status (Pending, In Progress, Completed): ");
            task.Status = GetValidInput(new[] { "Pending", "In Progress", "Completed" });

            Console.Write("Enter Category (Work, Personal, Study): ");
            task.Category = GetValidInput(new[] { "Work", "Personal", "Study" });

            tasks.Add(task);
            Console.WriteLine("\nTask added successfully.");

            Console.WriteLine("***********************************************");

        }

        public void ViewTasks()
        {
            Console.Write("Filter by category (leave blank for all): ");
            string category = Console.ReadLine();
            var filteredTasks = string.IsNullOrEmpty(category)
                ? tasks
                : tasks.Where(t => t.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();

            filteredTasks = filteredTasks.OrderBy(t => t.DueDate).ToList();

            if (!filteredTasks.Any())
            {
                Console.WriteLine("No tasks found.");
                return;
            }

            foreach (var task in filteredTasks)
            {
                Console.WriteLine($"\nTitle: {task.Title}");
                Console.WriteLine($"Description: {task.Description}");
                Console.WriteLine($"Due Date: {task.DueDate:yyyy-MM-dd}");
                Console.WriteLine($"Priority: {task.Priority}");
                Console.WriteLine($"Status: {task.Status}");
                Console.WriteLine($"Category: {task.Category}");
            }

            Console.WriteLine("***********************************************");

        }

        public void UpdateTask()
        {
            Console.Write("Enter Title of the task to update: ");
            string title = Console.ReadLine();

            UserTask task = tasks.FirstOrDefault(t => t.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (task == null)
            {
                Console.WriteLine("Task not found.");
                return;
            }

            Console.Write("Update Description (leave blank to keep current): ");
            string description = Console.ReadLine();
            if (!string.IsNullOrEmpty(description)) task.Description = description;

            Console.Write("Update Due Date (leave blank to keep current): ");
            string dueDateInput = Console.ReadLine();
            if (!string.IsNullOrEmpty(dueDateInput))
            {
                DateTime updatedDueDate;
                if (DateTime.TryParse(dueDateInput, out updatedDueDate))
                {
                    task.DueDate = updatedDueDate;
                }
                else
                {
                    Console.WriteLine("Invalid date format. Keeping the current Due Date.");
                }
            }

            Console.Write("Update Priority (Low, Medium, High, leave blank to keep current): ");
            while (true)
            {
                string priority = Console.ReadLine();
                if (string.IsNullOrEmpty(priority))
                {
                    break;
                }

                if (new[] { "Low", "Medium", "High" }.Contains(priority, StringComparer.OrdinalIgnoreCase))
                {
                    task.Priority = priority;
                    break;
                }

                Console.WriteLine("Invalid input. Please enter one of the following values: Low, Medium, High, or leave blank to keep current.");
            }

            Console.Write("Update Status (Pending, In Progress, Completed, leave blank to keep current): ");
            while (true)
            {
                string status = Console.ReadLine();
                if (string.IsNullOrEmpty(status))
                {
                    break;
                }

                if (new[] { "Pending", "In Progress", "Completed" }.Contains(status, StringComparer.OrdinalIgnoreCase))
                {
                    task.Status = status;
                    break;
                }

                Console.WriteLine("Invalid input. Please enter one of the following values: Pending, In Progress, Completed, or leave blank to keep current.");
            }

            Console.Write("Update Category (Work, Personal, Study, leave blank to keep current): ");
            string category = Console.ReadLine();
            if (!string.IsNullOrEmpty(category) && new[] { "Work", "Personal", "Study" }.Contains(category, StringComparer.OrdinalIgnoreCase))
            {
                task.Category = category;
            }

            Console.WriteLine("Task updated successfully.");

            Console.WriteLine("***********************************************");

        }

        public void DeleteTask()
        {
            Console.Write("Enter Title of the task to delete: ");
            string title = Console.ReadLine();

            UserTask task = tasks.FirstOrDefault(t => t.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (task == null)
            {
                Console.WriteLine("Task not found.");
                return;
            }

            tasks.Remove(task);
            Console.WriteLine("Task deleted successfully.");

            Console.WriteLine("***********************************************");

        }

        public void SearchTasks()
        {
            Console.Write("Enter a keyword to search for: ");
            string keyword = Console.ReadLine();

            var matchedTasks = tasks.Where(t => t.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                                                t.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!matchedTasks.Any())
            {
                Console.WriteLine("No tasks matched the keyword.");
                return;
            }

            foreach (var task in matchedTasks)
            {
                Console.WriteLine($"\nTitle: {task.Title}");
                Console.WriteLine($"Description: {task.Description}");
                Console.WriteLine($"Due Date: {task.DueDate:yyyy-MM-dd}");
                Console.WriteLine($"Priority: {task.Priority}");
                Console.WriteLine($"Status: {task.Status}");
                Console.WriteLine($"Category: {task.Category}");
            }

            Console.WriteLine("***********************************************");
        }

        public void ViewProgress()
        {
            int totalTasks = tasks.Count;
            if (totalTasks == 0)
            {
                Console.WriteLine("No tasks available.");
                return;
            }

            int completedTasks = tasks.Count(t => t.Status.Equals("Completed", StringComparison.OrdinalIgnoreCase));
            double completionRate = (double)completedTasks / totalTasks * 100;

            Console.WriteLine($"\nTotal Tasks: {totalTasks}");
            Console.WriteLine($"Completed Tasks: {completedTasks}");
            Console.WriteLine($"Completion Rate: {completionRate:F2}%");

            Console.WriteLine("***********************************************");
        }

        public async Task SaveTasksAsync()
        {
            try
            {
                string json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions
                {
                    WriteIndented = true
                });
                await File.WriteAllTextAsync(FilePath, json);
                Console.WriteLine("Tasks saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving tasks: {ex.Message}");
            }
        }

        public async Task LoadTasksAsync()
        {
            if (File.Exists(FilePath))
            {
                try
                {
                    string json = await File.ReadAllTextAsync(FilePath);
                    var loadedTasks = JsonSerializer.Deserialize<List<UserTask>>(json);
                    if (loadedTasks != null) tasks.AddRange(loadedTasks);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading tasks: {ex.Message}");
                }
            }
        }

        private static DateTime GetValidDate()
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (DateTime.TryParse(input, out DateTime date))
                {
                    return date;
                }
                Console.Write("Invalid date format. Please enter a valid date (yyyy-MM-dd): ");
            }
        }

        private static string GetValidInput(string[] validOptions)
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (validOptions.Contains(input, StringComparer.OrdinalIgnoreCase))
                {
                    return input;
                }
                Console.Write($"Invalid input. Please enter one of the following: {string.Join(", ", validOptions)}: ");
            }
        }
    }
}
