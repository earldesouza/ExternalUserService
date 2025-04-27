# ExternalUserService

A .NET class library to fetch user data from the public API at [reqres.in](https://reqres.in/).

## Features

- Fetch single user by ID
- Fetch all users (handles pagination)
- Async HTTP calls using `HttpClient` and `IHttpClientFactory`
- Error handling for API failures
- Configurable base URL
- Unit tests included

## Getting Started

### Prerequisites

- .NET 6 SDK or later

### Build and Run Demo

git clone https://github.com/yourusername/ExternalUserService.git
cd ExternalUserService
dotnet build
cd ExternalUserService.Demo
dotnet run