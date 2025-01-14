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

## Technologies Used

- **C#**: The application is developed in C#, using object-oriented programming principles to structure the task management functionality.
- **Visual Studio 2022**: The development environment for building and testing the application.
- **JSON**: Data is serialized to and deserialized from JSON format using `System.Text.Json`.
