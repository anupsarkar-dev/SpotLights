## To change Database Provider
Update provider and connection string in the `appsettings.json`:


#### Microsoft SqlServer
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


## When a change to an entity field requires a database migration

The database migration is stored in the src/SpotLights.Data/Migrations  directory. 


## Migration Command 

``` shell
# Jump to root solution directory
Solution root -> Right Click -> Open In Terminal

# Before proceed make sure you have dotnet-ef tools are installed in your system
dotnet tool install --global dotnet-ef

# Create New Migration
dotnet ef migrations add  Migration_Name   --project=src\SpotLights.Data\SpotLights.Data.csproj --startup-project=src\SpotLights\SpotLights.csproj --context ApplicationDbContext

# Update Database
dotnet ef database update   --project=src\SpotLights.Data\SpotLights.Data.csproj --startup-project=src\SpotLights\SpotLights.csproj --context ApplicationDbContext

```

### Warn 
Do not add or delete database migration at will. After the application generates data, random migration may cause data loss. This project will automatically apply the migration when it starts.
