# Backend

## Db setup

1. Docker setup:

- I use this docker command for first time setup for my local dev:

`docker run --name coffee-shop-db -d -p 5432:5432 -e POSTGRES_DB=coffee-shop-db -e POSTGRES_PASSWORD=mysecretpassword -d postgres`

- To start the container you can use:

` docker container start coffee-shop-db`

2. Add the connection string to either secrets/appsettings/local.settings/env variable

`ConnectionString: "Server=127.0.0.1;Port=5432;Database=coffee-shop-db;User Id=postgres;Password=mysecretpassword;Include Error Detail=true"`

3. Connect: 

- Note: If connecting from `Azure Data Studio` on windows make sure you use `host.docker.internal` as server name
