# Rate API

This project showcases an API designed to calculate a price of a given date range

## Getting Started

These instructions will help you get up and running with the rate API on your local machine.

### Prerequisites

* [Docker](https://www.docker.com/community-edition) (Docker for [Mac](https://www.docker.com/docker-mac) \ [Windows](https://www.docker.com/docker-windows) will cover this requirement)
* [Docker Compose](https://docs.docker.com/compose/install/)  (Docker for [Mac](https://www.docker.com/docker-mac) \ [Windows](https://www.docker.com/docker-windows) will cover this requirement too)
* [.NET Core SDK](https://www.microsoft.com/net/download) (this will be used to execute the tests)
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
If you get an error at this step such as "port has already been allocated", the project is trying to use a resource already in use by your computer, try changing up the host port numbers defined in [docker-compose.yml](docker-compose.yml). Consult the [Docker Compose networking documenation](https://docs.docker.com/compose/networking/) for additional information.

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

From the /tests directory execute the .NET CLI command to test; if the containers are not running, the integration tests will fail

```
dotnet test
```

## A few of the libraries and tools used in this project

* [ASP.NET Core 2.0](https://github.com/aspnet/Home) 
* [Npgsql.EntityFrameworkCore.PostgreSQL](https://github.com/npgsql/Npgsql.EntityFrameworkCore.PostgreSQL) 
* [prometheus-net.AspNetCore](https://github.com/prometheus-net/prometheus-net) 