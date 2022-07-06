# TVShows Demo API - ASP.NET Web Web API

This demo project was created using Microsoft .NET 6 and C#. The project consists of a Rest Web API that accesses a database containing a list of TV series. All CRUD operations are implemented. It is possible to add, update, delete and query TV genres and shows. 

## Tech Stack
- .NET 6.0
- C#
- ASP.NET Web API
- Entity Framework
- PostgreSQL Database
- Docker Compose

## Design Patterns
- MVC
- Generic Repository
- Specification
- Unit of Work

## Project Features
- Pagination
- Filters
- Exception Middleware
- Error handling
- Code First Entity

## 🔧 Runing the project

### Requirements:
- Docker
- VSCode
- Visual Studio Code Remote - Containers Extension

### Steps:

- Clone the repo on your local machine
- Make sure you have Docker installed on your local machine
- Navigate on your command line interface to the root folder of the project
- Enter the command: code .
- VSCode shoud detect the devcontainer and ask if it should reopen it in a remote container. Click yes.
- Wait while Docker pulls and builds all the images. Once it is ready you should see /workspace on the VSCode integrated terminal
- Navigate to the API folder
- Run the commnad: dotnet run
- The API should be available at https://localhost:5001

<p align="center">Copyright © 2022 João Aroeira</p>