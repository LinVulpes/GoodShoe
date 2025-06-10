# GoodShoe - Shoe E-Commerce Web Application
**GoodShoe**, a modern online store for sports shoes built as a full-stack web application using ASP .NET Core and Bootstrap. This project aims to recreate a real-world e-commerce platform designed to showcase and sell sport shoes. 
User can browse, search, filter, and purchase shoes, track their orders, and personalize their user profile.

## Project Overview
GoodShoe is a collaborative effort by a team from The University of Newcastle, Australia. The platform allows users to explore a catalog of shoes, apply sorting and filtering options, and manage their shopping experience with a mock checkout process.

## Features

- **Home Page**: Browse a catalog of shoes with navigation, search, filtering (by category, price range), and sorting (name A-Z, Z-A, price low-high, high-low) options. Click a product to view details.
- **User Profile Page**: Register, log in, log out, and manage account details, including order history.
- **Shopping Cart & Orders**: Add items to a cart, view and remove products, proceed through a mock checkout, and track past orders with a confirmation popup.
- **Admin Dashboard**: Restricted access for admins to manage users, products (add/edit/delete, stock status), and orders (view/update status).

## Technologies

- **Frontend**: HTML, CSS, Bootstrap, Razor
- **Backend**: ASP.NET Core (MVC), C#, JavaScript
- **Authentication**: ASP.NET Identity
- **Database**: SQLite (with plans for SQL Server)
- **Development Tools**: Visual Studio / JetBrains Rider
- **Version Control**: Git & GitHub
- **Hosting**: Localhost (Azure optional)

## Installation
1. **Clone the Repository**:
   ```bash
   git clone https://github.com/your-username/GoodShoe.git
   cd GoodShoe

2. **Install Dependencies**:
   - Ensure you have .NET SDK installed.
   - Restore packages:
   ```^Sbash
   dotnet restore

3. **Configure the Database**:
   - Update the appsettings.json file with your local database or SQLite connection string:
   ```^Sjson
   {
  	"ConnectionStrings": {
    	"DefaultConnection": "Data Source=GoodShoe.db"
  	}
   }

   - Run the initial migration to create the database:
   ```^Sbash
   dotnet ef database update

4. **Run the Application**:
   ```^Sbash
   dotnet run

   Open your browser and navigate to your localhost port (e.g. https://localhost:5001).




Build a full-stack web application where users can browse, search, and buy shoes.

StepUp is a modern online store for sport shoes, built using ASP .NET Core and Bootstrap. 
The purpose of this project is to recreate a real-world online shopping website designed to showcase and sell sport shoes online. 
The platform is designed mainly for customers of both genders with a focus on athletes and everyday users. 
Users can browse, search, and filter shoes by brand, size or price while allowing viewing products and purchasing them online.  
Users can also track their purchases through an order history page, and maintain a profile to personalize their experience.
