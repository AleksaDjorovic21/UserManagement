<<<<<<< HEAD
# ProductApp

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 18.2.2.

## Development server

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The application will automatically reload if you change any of the source files.

## Code scaffolding

Run `ng generate component component-name` to generate a new component. You can also use `ng generate directive|pipe|service|class|guard|interface|enum|module`.

## Build

Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory.

## Running unit tests

Run `ng test` to execute the unit tests via [Karma](https://karma-runner.github.io).

## Running end-to-end tests

Run `ng e2e` to execute the end-to-end tests via a platform of your choice. To use this command, you need to first add a package that implements end-to-end testing capabilities.

## Further help

To get more help on the Angular CLI use `ng help` or go check out the [Angular CLI Overview and Command Reference](https://angular.dev/tools/cli) page.
=======
# Product Management App

A simple ASP.NET Core web application for managing products with user authentication and role-based access control.

## Table of Contents

1. [Prerequisites](#prerequisites)
2. [Installation](#installation)
3. [Configuration](#configuration)
4. [Running the Application](#running-the-application)
5. [Usage](#usage)
6. [Role Information](#role-information)
7. [Database Migrations](#database-migrations)
8. [Testing](#testing)


## Prerequisites

Before you begin, ensure you have the following installed on your machine:

- [.NET 6.0 SDK]
- [SQL Server]
- [Visual Studio 2022]

## Installation

1. Clone the repository:
   ```bash
   git clone <repository-url>
   cd ProductManagementApp
   ```

2. Restore the packages:
   ```bash
   dotnet restore
   ```

3. If you are using Visual Studio, open the solution file (`ProductManagementApp.sln`) directly.

## Configuration

1. **Database Connection:**
   - Open `appsettings.json` and set up the database connection:
   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\mssqllocaldb;Database=ProductAppDb;Trusted_Connection=True;MultipleActiveResultSets=true"
   }
   ```
   - Ensure that the connection string matches your SQL Server configuration.

2. **Identity Settings:**
   - The application uses ASP.NET Identity for user authentication. You may customize settings in the `Program.cs` if needed.

## Running the Application

To run the application, execute the following command in your terminal:
```bash
dotnet run
```

## Usage

1. **Access the Application:**
   - Open your web browser and navigate to `http://localhost:5099`.
   
2. **Registration and Login:**
   - Use the registration form to create a new user account.
   - Log in using your registered credentials.

3. **Product Management:**
   - As an Admin user, you can create, edit, and delete products.
   - Regular users can only view the list of products.
   - Also Admin user have a feature to give permission to a users.

## Role Information

The application utilizes role-based access control. The defined roles are:

- **Admin:** 
  - Can create, edit, and delete products.
  - **Default Admin Credentials:**
    - **Email:** Admin
    - **Password:** Admin123
  
- **User:**
  - Can only view the list of products.

## Database Migrations

To apply the database migrations and create the necessary tables, follow these steps:

1. Open your terminal and navigate to the project directory.
2. Run the following command:
   ```bash
   dotnet ef database update
   ```

This command will create the database and apply all the migrations defined in your application.

## Testing

To run the unit tests for the application, execute the following command in the test project directory:
```bash
dotnet test
```


>>>>>>> dfe82d845dbfe7088f471e6d16cb17b473a6a8de
