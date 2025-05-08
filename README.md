# News App

Application developed for education purposes

## Intro

As I'm a proponent of Domain Driven Design, I've decided to use some techniques and tools from DDD to develop the API for managing and delivering news content.

The solution follows a clean architecture pattern and consists of the following projects:

- `Api`: The web API layer that handles HTTP requests and responses
- `Core`: Contains the business logic, domain entities, and core interfaces
- `Infrastructure`: Implementation of infrastructure concerns (data access, external services)
- `UnitTests`: Contains unit tests for the application

## Getting Started

Technical requirements:
1. .NET 9.0 SDK
2. PostgreSQL instance
   1. Connection string to the DB can be set through user secrets with the standard key _ConnectionString on the `Api` project

After running the migrations from the `Infrastructure` project (this project also requires a connection string), a user with and ID of 1 should be entered manually in the `client`.`user` table. 

### Auth

This API has two endpoints, one of which is public and the other expects a bearer token authentication header. The authentication token can be generated with the following command:

```
dotnet user-jwts create --name 1
```

This authentication mechanism is used only for development purposes and for production a proper OAuth2 implementation should be used.
