# Rate API

This project showcases an API designed to calculate a price of a given date range

## Getting Started

These instructions will help get up and running with the rate API on your local machine.

### Prerequisites

* [Docker](https://www.docker.com/community-edition)
* [Docker Compose](https://docs.docker.com/compose/install/)
* [.NET Core SDK](https://www.microsoft.com/net/download) (this will be used to run database migrations and execute the tests)
* Not required, but [Postman](https://www.getpostman.com/apps) is recommended for API calls

### Installing

Clone this repository locally

```
git clone git@github.com:bgnicoll/rates-api.git
```

Build the containers

```
docker-compose build
```

Run the containers

```
docker-compose up
```

If this is your first time running the application, you will need to set up the database tables before using the API. Don't worry! You won't need to install or configure anything special, the database is running inside a container! You can run the migration by opening up another terminal, navigating to the /api directory and running the Entity Framework migrations command
```
dotnet ef database update
```
Open your browser to http://localhost:8080 to find the [Swagger spec](https://swagger.io/) where you can see the API endpoints, upload new rates and then calculate the price for a time range. You can also use Postman once you get a feel for the API. The example POST data is below

```json
{
    "rates": [
        {
            "days": "mon,tues,thurs",
            "times": "0900-2100",
            "price": 1500
        },
        {
            "days": "fri,sat,sun",
            "times": "0900-2100",
            "price": 2000
        },
        {
            "days": "wed",
            "times": "0600-1800",
            "price": 1750
        },
        {
            "days": "mon,wed,sat",
            "times": "0100-0500",
            "price": 1000
        },
        {
            "days": "sun,tues",
            "times": "0100-0700",
            "price": 925
        }
    ]
}
``` 

### Monitoring
The Rate API has some built-in monitoring functionality. Navigate to http://localhost:8080/metrics to view some of these metrics. Included are also pre-configured instances of [Prometheus](https://prometheus.io/) and [Grafana](https://grafana.com/). You can find the UIs for these tools at http://localhost:9090 and http://localhost:3000 respectively. The default user for Grafana is 'admin' and the default password is 'admin'.

## Running the tests

From the /tests directory execute the .NET CLI command to test; the containers do not need to be running in order to execute these tests

```
dotnet test
```

## A few of the libraries and tools used in this project

* [ASP.NET Core 2.0](https://github.com/aspnet/Home) 
* [Npgsql.EntityFrameworkCore.PostgreSQL](https://github.com/npgsql/Npgsql.EntityFrameworkCore.PostgreSQL) 
* [prometheus-net.AspNetCore](https://github.com/prometheus-net/prometheus-net) 