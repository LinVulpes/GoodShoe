# GoodShoe - Shoe E-Commerce Web Application

**GoodShoe**, a modern online store for sports shoes built as a full-stack web application using ASP .NET Core and Bootstrap. This project aims to recreate a real-world e-commerce platform designed to showcase and sell sport shoes. 

## Project Overview

GoodShoe is a collaborative project developed by a team from the University Of NewCastle, Australia, using the **Scrum Agile methodology**. The platform is to demonstrates expertise in full-stack development, UI/UX design, and database management, tailored for both customers and administators. GoodShoe is designed for athlethes and everyday users with a seamless and engaging online sport shoe shopping experience.

## Features

- **Home Page**: Browse a catalog of shoes with navigation, search, filtering, and sorting options.
- **User Profile Page**: Register, log in, log out, and manage account details, including order history.
- **Shopping Cart & Orders**: Add items to a cart, view and remove products, proceed through a mock checkout, and track past orders with a confirmation popup.
- **Admin Dashboard**: Restricted access for admins to manage products, and orders.

## Technologies

- **Frontend**: HTML, CSS, Bootstrap, Razor
- **Backend**: ASP.NET Core (MVC), C#, JavaScript
- **Authentication**: ASP.NET Identity
- **Database**: SQL Server
- **Development Tools**: Visual Studio / JetBrains Rider
- **Version Control**: Git & GitHub
- **Hosting**: Localhost (Azure optional)

### Installation
1. **Clone the Repository** :
   ```bash
   git clone https://github.com/your-username/GoodShoe.git
   cd GoodShoe

2. **Install Dependencies** :
   - Ensure you have .NET SDK installed.
   - Restore packages:

   ```bash
   dotnet restore

3. **Configure the Database**:
   - Update the appsettings.json file with your local database or SQLite connection string :
   ```bash
   {                                                                                             "ConnectionStrings": {
        "DefaultConnection": "Data Source=GoodShoe.db"
        }
   }

- Run the Initial Migration to create the database :
   ```bash
   dotnet ef database update

4. **Run the Application** :
   ```bash
   dotnet run

Open your browser and navigate to your localhost port (e.g. https://localhost:5001).
