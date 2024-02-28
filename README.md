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

2. Build the application:

    ```bash
    dotnet build
   ```
      
3. Run the application:

    ```bash
    dotnet run
   ```

This will start the application on http://localhost:5055 by default.
swagger is added to make it easy to test the api, you can access swagger on http://localhost:5055/swagger/index.html

## Running Unit Tests

Unit tests are available to ensure the correctness of the service. You can run the tests using the following command:
```bash
dotnet test
```

## Cashing layer
The caching layer in the HackerNews API plays a crucial role in improving performance and reducing load on the Hacker News API server by storing frequently accessed data in memory. This README provides an overview of how caching is implemented in the HackerNews API.

### How Caching Works
For this example i unitized MemoryCache for simplicity , in a production example this can be replaced by something like memcached, redis or any other service that can provide better distributed cash 

#### MemoryCacheService
The caching layer utilizes the MemoryCacheService, which is responsible for storing and retrieving cached data in memory. This service is integrated into the HackerNews API to cache responses from the Hacker News API server.

#### Caching Strategy
When a request is made to fetch the best stories from the Hacker News API, the caching layer first checks if the requested data is available in the cache.
If the data is found in the cache and is still valid (not expired), it is returned directly from the cache.
If the data is not found in the cache or has expired, the caching layer fetches the data from the Hacker News API server, stores it in the cache for future use, and returns it to the caller.

#### Cache Expiration
To ensure that cached data remains up-to-date, each cached item is associated with a time-to-live (TTL) value. When fetching data from the Hacker News API server, the caching layer sets an appropriate expiration time based on the TTL configured for the cache.


## API Usage
Once the application is running, you can access the API to retrieve the best n stories from Hacker News.

## Get Best Stories
To retrieve the best n stories, make a GET request to the following endpoint:
```bash
GET http://localhost:5055/hackernews/best/{n}
```
Replace {n} with the number of stories you want to retrieve.

Example:
```bash
GET http://localhost:5055/hackernews/best/5
```

This will return an array of the best 5 stories in descending order of score.

## Dependencies
- Microsoft.Extensions.Http
- Newtonsoft.Json
- Microsoft.Extensions.Caching.Memory
- NUnit
- Moq

