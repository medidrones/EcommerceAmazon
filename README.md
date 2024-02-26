# EcommerceAmazon
Ecommerce Amazon Clone con ASP NET Core .NET 7 y React), utilizando Clean Architecture


### Instalar instancia container banco de dados no Docker:
- docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=VaxiDrez2005$" -p 1433:1433 --name "sql-server-jmed" -d mcr.microsoft.com/mssql/server

### Migrations:
- Importante saber qual versão do SDK NET7, está instalado no seu computador, para utilizar o dotnet-ef:
  - dotnet --version: 7.0.400
  - https://www.nuget.org/packages/dotnet-ef/7.016
    - dotnet tool uninstall dotnet-ef --global => Para desinstalar as versões anteriores do dotnet-ef.
    - dotnet tool install --global dotnet-ef --version 7.0.16
- Executar a Migrations:
  - dotnet ef migrations add InitMigration -p src/Infrastructure/ -s src/Api/

### Acessar banco SQL no Docker:
- Acessar: Microsoft SQL Server Management Studio
- Configurar Connect to Server:
  - Server type: Database Engine  
  - Serve name: localhost
  - Authentication: SQL Server Authentication
  - Login: sa
  - Password: VaxiDrez2005$