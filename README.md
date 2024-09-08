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

### Adding a new task
```bash
add Buy groceries
```
### Updating a task
```bash
update 1 Buy groceries and cook dinner
```
### Deleting a task
```bash
delete 1
```
### Marking a task as in progress
```bash
mark in-progress 1
```
Marking a task as done
```bash
mark done 1
```
Listing all tasks
```bash
list
```
### Listing tasks by status
#### Listing "todo" tasks
```bash
list todo
```

#### Listing "done" tasks
```bash
list done
```
#### Listing "in-progress" tasks
```bash
list in-progress
```

## Contact

Project Maintainer: Rabee Qabaha  
Email: [Qabaha.Rabee@outlook.com](mailto:Qabaha.Rabee@outlook.com)  
LinkedIn: [www.linkedin.com/in/rabee-q](https://www.linkedin.com/in/rabee-q)





