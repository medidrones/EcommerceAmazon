# EcommerceAmazon
Ecommerce Amazon Clone con ASP NET Core .NET 7 y React), utilizando Clean Architecture


### Instalar una instancia de contenedor de base de datos en Docker:
- docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=VaxiDrez2005$" -p 1433:1433 --name "sql-server-jmed" -d mcr.microsoft.com/mssql/server

### Migrations:
- Es importante saber qué versión del SDK de NET7 está instalada en su computadora para poder usar dotnet-ef:
  - dotnet --version: 7.0.400
  - https://www.nuget.org/packages/dotnet-ef/7.016
    - dotnet tool uninstall dotnet-ef --global => Para desinstalar versiones anteriores de dotnet-ef.
    - dotnet tool install --global dotnet-ef --version 7.0.16
- Ejecutar migraciones:
  - dotnet ef migrations add InitMigration -p src/Infrastructure/ -s src/Api/

### Acceder a la base de datos SQL en Docker:
- Acceso: Microsoft SQL Server Management Studio
- Configurar Connect to Server:
  - Server type: Database Engine  
  - Serve name: localhost
  - Authentication: SQL Server Authentication
  - Login: sa
  - Password: VaxiDrez2005$