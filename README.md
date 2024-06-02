<br>
<h3 align="center">SpotLights | Developer Community Blog Portal </h3>
<p align="center">
    SpotLights is a self-hosted open source publishing platform written in .NET Core 8.0 and Blazor WebAssembly with Clean Architecture. 
    It can be used to quickly and easily set up a lightweight, but fully functional personal or community blog.
</p>
<br>

  
## UI

### Homepage

![Project flow diagram](./homepage.png)


### Blog view

![Project flow diagram](./blog.png)


### Dashboard

![Project flow diagram](./dashboard.png)


### Post manager

![Project flow diagram](./dashboard2.png)




## Installation and Development
If you want to customize the SpotLights, or contribute:

1. Download, fork, or clone the repository.
2. [Download](https://dotnet.microsoft.com/zh-cn/download) .NET 8.0 SDK Choose to install the system version on your host. 
3. Nodejs 14 and above and install it on your host. [Download](https://nodejs.org/) 
4. Requires RDBMS whichever you prefer (Mssql Server, Postgres or Sqlite all are supported)
5. Requires Redis for caching
6. Set SpotLights.Application as start up project.
7. Open the project with your favorite IDE (VS Code, Visual Studio, Atom, etc).
8. Run the app with your IDE or these commands:
```
$ cd /your-local-path/SpotLights/src/SpotLights.Application
$ dotnet run
```
Then you can open `localhost:5000` with your browser
 


 ## To change Database Provider (Choose any preferred database)
Update provider and connection string in the `appsettings.json`:


#### Microsoft Sql Server
``` json
"SpotLights": {
   "DbProvider": "Mssql",
   "ConnString": "Data Source=mssql; User Id=sa; Password=Password; Initial Catalog=SpotLights;TrustServerCertificate=True",
   ...
}
```
In the latest version of sql server connection, SqlClient will perform a secure connection by default, and you need to add a server certificate to the system. 

#### SQLite
``` json
"SpotLights": {
  "DbProvider": "Sqlite",
  "ConnString": "Data Source=App_Data/SpotLights.db",
   ...
}
```
It is recommended to put the database file under the App_Data folder. The logs and local pictures in the project will be stored in this path for persistence.


#### Postgres
``` json
"SpotLights": {
   "DbProvider": "Postgres",
   "ConnString": "Host=postgres;Username=postgres;Password=password;Database=SpotLights;",
   ...
}
```
In the above example, ConnString requires you to fill in the correct database host address username and password to connect normally



## In Memory Caching Database
Currently redis is configured and required for running the application

#### Redis
``` json
"SpotLights": {
   "Redis": "127.0.0.1:6379,defaultDatabase=0",
   ...
}
```

## When a change to an entity field requires a database migration

The database migration is stored in the src/SpotLights.Data/Migrations  directory. 


## Migration Command 
Should run all the commands in root solution directory (Solution root -> Right Click -> Open In Terminal)

``` shell

# Before proceed make sure you have dotnet-ef tools are installed in your system
dotnet tool install --global dotnet-ef

# Create New Migration
dotnet ef migrations add  Migration_Name   --project=src\SpotLights.Data\SpotLights.Data.csproj --startup-project=src\SpotLights\SpotLights.csproj --context ApplicationDbContext

# Update Database
dotnet ef database update   --project=src\SpotLights.Data\SpotLights.Data.csproj --startup-project=src\SpotLights\SpotLights.csproj --context ApplicationDbContext

```


## Project Structure

![Project flow diagram](./flow.png)


``` shell

Solution
├── SpotLights.Admin(Contains Admin Dashboard related files)
│   ├── Webassembly dependencies
│   ├── Views, Components, Pages
│   └── Assests, wwwroot
│ 
├── SpotLights.Application (Startup Web Project)
│   ├── Api Controller
│   ├── View Controller
│   └── Views
│   
├── SpotLights.Core
│   ├── Services
│   └── Views
│
├── SpotLights.Infrastructure
│   ├── Repositories
│   ├── Identity
│   ├── Caches
│   ├── Managers
│   └── Providers
│   
├── SpotLights.Data
│   ├── DbContext
│   ├── Entity Configurations
│   └── Migrations
│
├── SpotLights.Domain
│   ├── Entity Model
│   ├── Dao
│   └── Dto
│
└── SpotLights.Shared
    ├── Constants
    ├── Enums
    ├── Dto
    ├── Extensions
    ├── Viewmodels
    ├── Resources
    └── Helpers


```
### Warn 
Do not add or delete database migration at will. After the application generates data, random migration may cause data loss. This project will automatically apply the migration when it starts.

