# Task Tracker

A simple Command Line Interface (CLI) application to track and manage tasks. The application allows you to add, update, delete, and list tasks. Tasks are stored in a JSON file, ensuring a persistent, local storage solution.

## Features

- Add new tasks with a description.
- Mark tasks as "in progress" or "done".
- List tasks by status (e.g., all tasks, done, in progress).
- Delete tasks.
- Update task descriptions.

## Table of Contents

- [Installation](#installation)
- [Commands](#commands)
- [Contact](#Contact)

## Installation

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- A text editor or IDE like [Visual Studio Code](https://code.visualstudio.com/) or [Rider](https://www.jetbrains.com/rider/)

### Clone the Repository

```bash
git clone https://github.com/username/Task-Tracker.git
cd Task-Tracker
```
## Commands

The following commands are available in the Task Tracker CLI:

- **list**:  
  Lists all tasks.  
  Example:  
  `list`

- **list todo**:  
  Lists tasks with a "todo" status.  
  Example:  
  `list todo`

- **list done**:  
  Lists tasks with a "done" status.  
  Example:  
  `list done`

- **list in-progress**:  
  Lists tasks with an "in-progress" status.  
  Example:  
  `list in-progress`

- **add**:  
  Adds a new task with a description.  
  Example:  
  `add "Task description"`
- 
- **delete**:  
  Deletes a task by its ID.  
  Example:  
  `delete 1` (This deletes the task with ID 1)

- **update**:  
  Updates a task's description by its ID.  
  Example:  
  `update 1 "New task description"` (This updates the task with ID 1)

- **mark-done**:  
  Marks a task as "done" by its ID.  
  Example:  
  `mark done 1` (This marks the task with ID 1 as done)

- **mark-in-progress**:  
  Marks a task as "in-progress" by its ID.  
  Example:  
  `mark in-progress 1` (This marks the task with ID 1 as in-progress)

- **mark-todo**:  
  Marks a task as "todo" by its ID.  
  Example:  
  `mark todo 1` (This marks the task with ID 1 as todo)


## Contact

Project Maintainer: Rabee Qabaha  
Email: [Qabaha.Rabee@outlook.com](mailto:Qabaha.Rabee@outlook.com)  
LinkedIn: [www.linkedin.com/in/rabee-q](https://www.linkedin.com/in/rabee-q)





