# Task Manager Application

## Overview
The Task Manager application provides an intuitive interface to manage tasks effectively. With features like adding, updating, deleting, and viewing tasks, it allows users to stay on top of their responsibilities. The app also provides task search, progress tracking, and the ability to save/load tasks to/from a JSON file for persistent storage across sessions.

## Features

1. **Add a Task**  
   The user can add a new task by providing the following details:
   - **Title**: A brief description of the task.
   - **Description**: A detailed explanation of what the task involves.
   - **Due Date**: The deadline for the task in the format (yyyy-MM-dd).
   - **Priority**: Select from **Low**, **Medium**, or **High** to indicate the urgency of the task.
   - **Status**: Choose from **Pending**, **In Progress**, or **Completed** to indicate the current state of the task.
   - **Category**: Categorize tasks as **Work**, **Personal**, or **Study**.

   These inputs ensure that every task is organized and easy to track.

2. **View Tasks**  
   The application displays tasks, with the option to filter them by category. Tasks are sorted by due date, ensuring that the most urgent tasks are displayed first. If no filter is applied, all tasks are shown.

3. **Update a Task**  
   Modify the details of an existing task. You can update the following fields:
   - **Description**
   - **Due Date**
   - **Priority**
   - **Status**
   - **Category**

   This feature ensures that changes to a task are easy to make, and only relevant fields need to be updated.

4. **Delete a Task**  
   Delete tasks from the system by entering their title. If the task exists, it will be removed from the task list.

5. **Search Tasks**  
   Find tasks that contain a specific keyword in either their title or description. The search is case-insensitive and uses the `FirstOrDefault` method to retrieve the first task that matches the keyword, offering fast results.

6. **View Progress**  
   Track the completion progress of all tasks. The application calculates the percentage of completed tasks out of the total number of tasks, providing a clear insight into how much work has been finished.

7. **Save/Load Tasks**  
   Tasks are saved to a local JSON file (`tasks.json`). This feature ensures that tasks are persistent across sessions. When the app is reopened, tasks can be loaded from the file so users can continue managing their tasks without losing data.

## Technical Details

- **Programming Language**: The application is written in **C#** using **Visual Studio 2022** as the Integrated Development Environment (IDE).
- **Data Storage**: Tasks are saved in a JSON file using `System.Text.Json`. This enables easy serialization and deserialization of task data for storage and retrieval.
- **Methods**: 
  - The `FirstOrDefault` method is used for searching tasks by title, ensuring that we fetch the first task that matches the search criteria or return `null` if no match is found.
  - The `OrderBy` method is employed to sort tasks by due date in ascending order, making sure that tasks with earlier due dates are shown first.
  - The `GetValidInput` method is used to ensure valid inputs for the task's priority, status, and category, prompting the user until a correct option is selected.
  - The `GetValidDate` method is used to ensure the user inputs a valid date in the required format.

## Method Breakdown

### 1. **ViewProgress**  
   The `ViewProgress` method is used to calculate and display the completion progress of tasks. It works by:
   - **Counting Total Tasks**: First, the total number of tasks is counted using `tasks.Count`.
   - **Counting Completed Tasks**: It filters tasks where the `Status` is set to `"Completed"` using `tasks.Count(t => t.Status.Equals("Completed", StringComparison.OrdinalIgnoreCase))`.
   - **Calculating Completion Rate**: It then calculates the percentage of tasks that are completed using the formula:
     ```csharp
     double completionRate = (double)completedTasks / totalTasks * 100;
     ```
   - **Displaying Progress**: Finally, it displays the total number of tasks, the number of completed tasks, and the completion rate to the user.

   This method provides users with a clear insight into how much work has been finished, offering a quick overview of task completion.

### 2. **SearchTasks**  
   The `SearchTasks` method allows users to search for tasks based on a keyword entered by the user. It performs the following steps:
   - **Getting User Input**: The method prompts the user to enter a keyword for searching tasks, which is read using `Console.ReadLine()`.
   - **Filtering Tasks**: It filters the `tasks` list using LINQ’s `Where` method, looking for tasks where either the `Title` or `Description` contains the entered keyword. The comparison is case-insensitive using `StringComparison.OrdinalIgnoreCase`.
     ```csharp
     var matchedTasks = tasks.Where(t => t.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                                         t.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();
     ```
   - **Displaying Results**: If no tasks match the keyword, a message is displayed to the user. Otherwise, it prints the details of each matched task, including the title, description, due date, priority, status, and category.

   This feature helps users quickly find tasks based on keywords, making it easy to locate specific tasks by title or description.

### 3. **ViewTasks**  
   The `ViewTasks` method is used to view all tasks or filter tasks by category. It operates as follows:
   - **Getting Category Filter**: The method prompts the user to optionally filter tasks by category. If no category is entered, it shows all tasks.
   - **Filtering Tasks**: If a category is specified, it filters the `tasks` list using `Where`, matching the category (case-insensitive):
     ```csharp
     var filteredTasks = string.IsNullOrEmpty(category)
         ? tasks
         : tasks.Where(t => t.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
     ```
   - **Sorting by Due Date**: After filtering, tasks are sorted by their `DueDate` using `OrderBy` to ensure that tasks with earlier due dates appear first.
     ```csharp
     filteredTasks = filteredTasks.OrderBy(t => t.DueDate).ToList();
     ```
   - **Displaying Tasks**: If no tasks match the filter or sorting criteria, a message is displayed. Otherwise, it prints the details of each filtered task, including the title, description, due date, priority, status, and category.

   This method allows users to view tasks in an organized way, either showing all tasks or filtered by category and sorted by due date.

## Technologies Used

- **C#**: The application is developed in C#, using object-oriented programming principles to structure the task management functionality.
- **Visual Studio 2022**: The development environment for building and testing the application.
- **JSON**: Data is serialized to and deserialized from JSON format using `System.Text.Json`.
