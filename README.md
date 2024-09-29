# Product Management App

The Product Management App is a full-featured web application built using ASP.NET Core on the backend and Angular for the frontend. The application allows for comprehensive product management, user authentication, and role-based access control.

## Table of Contents

1. [Prerequisites](#prerequisites)
2. [Installation](#installation)
3. [Configuration](#configuration)
4. [Database Migrations](#database-migrations)
5. [Running the Application](#running-the-application)
6. [Usage](#usage)
7. [Role Information](#role-information)
8. [Testing](#testing)

## Prerequisites

Before you begin, ensure you have the following installed on your machine:

Angular:
- [Angular 18]
- [Node.js] - Ensure you have Node.js installed. If not, you can download it from `https://nodejs.org/`
- [Angular CLI]

C# .Net:
- [.NET 8.0 SDK]
- [SQL Server]
- [Visual Studio 2022] //edit

## Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/AleksaDjorovic21/UserManagement.git
   cd UserManagement
   ```

2. Restore the packages for C# .Net:
   ```bash
   dotnet restore
   ```

3. Install the packages for Angular:
   ```bash
   npm install -g @angular/cli
   ```

4. If you are using Visual Studio Code or Visual Studio 2022, open the solution file (`ProductApp.sln`).

## Configuration

1. **Database Connection:**
   - Open `appsettings.json` in `ProductApi.Api` and set up the database connection:
   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Server=<YOUR_SQLSERVER_HOST>;Database=ProductAppDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
   }
   ```
   - Ensure that the connection string matches your SQL Server configuration.

2. **Identity Settings:**
   - The application uses ASP.NET Identity for user authentication. You may customize settings in the `Program.cs` if needed. 

## Database Migrations 

To apply the database migrations and create the necessary tables, follow these steps:

1. Open your terminal and navigate to the `product-app/src/ProductApp.Infrastructure` directory.

2. Run the following command:
   ```bash
   dotnet ef migrations add InitialCreate
   ```

   ```bash
   dotnet ef database update
   ```

This command will create the database and apply all the migrations defined in your application.

## Running the Application

To run the C# .Net part of the application, open your terminal and navigate to the `product-app/ProductApp.Api` directory.
Than execute the following command in your terminal:
```bash
dotnet build
```

```bash
dotnet run
```

To run the Angular part of the application, open your terminal and navigate to the `product-app` directory.
Than execute the following command in your terminal:

```bash
ng build
```

```bash
ng serve
```

## Usage

1. **Access the Application:**
   - Open your web browser and navigate to `http://localhost:4200`. 
   
2. **Registration and Login:**
   - Use the registration form to create a new user account.
   - Log in using your registered credentials.

3. **Product Management:**
   - As an Admin user, you can create, edit, and delete products.
   - Admin user have a feature to give permission to a users and manage products.
   - Regular users can only view the list of products.

## Role Information

The application utilizes role-based access control. The defined roles are:

- **Admin:** 
  - Can create, edit, and delete products.
  - **Default Admin Credentials:**
    - **Email:** Admin
    - **Password:** Admin123
  
- **User:**
  - Can only view the list of products.

## Testing

To run the C# .Net tests, open your terminal and navigate to the `product-api` directory.
Than execute the following command in your terminal:

```bash
dotnet test
```

To run Angular tests, open your terminal and navigate to the `product-app` directory.
Than execute the following command in your terminal:

```bash
ng test
```

