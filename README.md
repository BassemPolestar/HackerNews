# HackerNewsAPI

This project implements a RESTful API using ASP.NET Core to retrieve the details of the best n stories from the Hacker News API.

## Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) installed on your machine.

## Getting Started

1. Clone the repository to your local machine:

   ```bash
   git clone https://github.com/yourusername/HackerNewsAPI.git
   ```

2. Navigate to the project directory:

    ```bash
    cd HackerNewsAPI
   ```

3. Restore the NuGet packages:

    ```bash
    dotnet restore
   ```

## Running the Application
You can run the application using the following steps:


1. Navigate to the project directory:

    ```bash
    cd HackerNewsAPI
   ```
   
2. Run the application:

    ```bash
    dotnet run --project HackerNewsAPI
   ```

This will start the application on http://localhost:5000 by default.

## Running Unit Tests

Unit tests are available to ensure the correctness of the service. You can run the tests using the following command:
```bash
dotnet test
```

## API Usage
Once the application is running, you can access the API to retrieve the best n stories from Hacker News.

## Get Best Stories
To retrieve the best n stories, make a GET request to the following endpoint:
```bash
GET http://localhost:5000/hackernews/best/{n}
```
Replace {n} with the number of stories you want to retrieve.

Example:
```bash
GET http://localhost:5000/hackernews/best/5
```

This will return an array of the best 5 stories in descending order of score.

## Dependencies
- Microsoft.Extensions.Http
- Newtonsoft.Json
- Microsoft.Extensions.Caching.Memory
- NUnit
- Moq

