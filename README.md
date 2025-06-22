# Cozy Comfort SOC Solution

This is a Service-Oriented Computing (SOC) based solution for "Cozy Comfort" blanket manufacturing company that streamlines the ordering process between Manufacturers, Distributors, and Sellers.

## Project Structure

```
CozyComfort/
├── CozyComfort.sln                          # Visual Studio Solution File
├── CozyComfort.Models/                      # Data Models Project
│   ├── User.cs                             # User entity with UserType enum
│   ├── Product.cs                          # Product entity for blankets
│   ├── Inventory.cs                        # Inventory management entity
│   └── Order.cs                            # Order and OrderItem entities
├── CozyComfort.Data/                       # Data Access Layer
│   └── CozyComfortDbContext.cs             # Entity Framework DbContext
├── CozyComfort.API/                        # Web API with ASMX Services
│   ├── Services/                           # ASMX Web Services Folder
│   │   ├── UserService.asmx                # User Management Service
│   │   ├── UserService.asmx.cs             # User Service Implementation
│   │   ├── ProductService.asmx             # Product Management Service
│   │   ├── ProductService.asmx.cs          # Product Service Implementation
│   │   ├── InventoryService.asmx           # Inventory Management Service
│   │   ├── InventoryService.asmx.cs        # Inventory Service Implementation
│   │   ├── OrderService.asmx               # Order Management Service
│   │   └── OrderService.asmx.cs            # Order Service Implementation
│   ├── Program.cs                          # API Startup Configuration
│   └── appsettings.json                    # Configuration Settings
└── CozyComfort.WindowsFormsClient/         # Windows Forms Client Application
    ├── Forms/                              # Windows Forms
    │   ├── LoginForm.cs                    # Login Form
    │   └── MainForm.cs                     # Main Dashboard Form
    ├── Services/                           # Service Client
    │   └── ServiceClient.cs                # HTTP client for ASMX services
    └── Program.cs                          # Client Application Entry Point
```

## System Architecture

### SOC Services Division

The system is divided into the following services:

1. **User Management Service** (`UserService.asmx`)
   - User authentication
   - User CRUD operations
   - User type management (Manufacturer, Distributor, Seller)

2. **Product Service** (`ProductService.asmx`)
   - Product catalog management
   - Product search and filtering
   - Product specifications management

3. **Inventory Service** (`InventoryService.asmx`)
   - Stock level management
   - Inventory tracking across all user types
   - Low stock alerts and reorder level management

4. **Order Service** (`OrderService.asmx`)
   - Order creation and management
   - Order status tracking
   - Order assignment to distributors/manufacturers

### Database Design

The solution uses a single SQL Server database with the following entities:

- **Users**: Stores manufacturer, distributor, and seller information
- **Products**: Blanket catalog with specifications
- **Inventory**: Stock levels for each user and product combination
- **Orders**: Customer orders with status tracking
- **OrderItems**: Individual items within orders

## Technology Stack

- **Backend**: .NET 6.0 Web API with ASMX Web Services
- **Frontend**: C# Windows Forms (.NET 6.0)
- **Database**: SQL Server with Entity Framework Core
- **Architecture**: Service-Oriented Architecture (SOA)

## Setup Instructions

### Prerequisites

- Visual Studio 2022
- .NET 6.0 SDK
- SQL Server LocalDB (included with Visual Studio)

### Database Setup

1. The application uses Entity Framework Code First approach
2. Database will be automatically created when the API starts
3. Sample data is seeded automatically including:
   - 3 users (Manufacturer, Distributor, Seller)
   - 3 sample blanket products
   - Initial inventory data

### Running the Application

1. **Start the API Server:**
   ```bash
   cd CozyComfort.API
   dotnet run
   ```
   The API will be available at `https://localhost:7000`

2. **Start the Windows Forms Client:**
   ```bash
   cd CozyComfort.WindowsFormsClient
   dotnet run
   ```

3. **Login Credentials:**
   - Username: Any text (demo mode)
   - Password: Any text (demo mode)
   - User Type: Select from dropdown (Manufacturer/Distributor/Seller)

## Service Operations

### User Service Operations
- `GetAllUsers()`: Retrieve all active users
- `GetUserById(int userId)`: Get user by ID
- `GetUsersByType(int userType)`: Get users by type
- `AuthenticateUser(string username, string password)`: User authentication
- `CreateUser(...)`: Create new user
- `UpdateUser(...)`: Update user information
- `DeactivateUser(int userId)`: Deactivate user

### Product Service Operations
- `GetAllProducts()`: Get all active products
- `GetProductById(int productId)`: Get product by ID
- `GetProductBySKU(string sku)`: Get product by SKU
- `SearchProducts(string searchTerm)`: Search products
- `CreateProduct(...)`: Create new product
- `UpdateProduct(...)`: Update product information
- `GetProductsByMaterial(string material)`: Filter by material
- `GetProductsBySize(string size)`: Filter by size

### Inventory Service Operations
- `GetInventoryByUser(int userId)`: Get user's inventory
- `GetStockQuantity(int userId, int productId)`: Get stock quantity
- `UpdateStock(int userId, int productId, int newQuantity)`: Update stock
- `AddStock(...)`: Add to stock
- `ReduceStock(...)`: Reduce stock
- `GetLowStockItems(int userId)`: Get low stock alerts
- `CheckAvailability(...)`: Check product availability

### Order Service Operations
- `GetOrdersBySeller(int sellerId)`: Get seller's orders
- `GetOrdersByDistributor(int distributorId)`: Get distributor's orders
- `GetOrdersByManufacturer(int manufacturerId)`: Get manufacturer's orders
- `CreateOrder(...)`: Create new order
- `AddOrderItem(...)`: Add item to order
- `UpdateOrderStatus(...)`: Update order status
- `AssignDistributor(...)`: Assign order to distributor
- `AssignManufacturer(...)`: Assign order to manufacturer

## Business Process Flow

1. **Seller** receives customer order
2. **Seller** checks local inventory
3. If unavailable, **Seller** contacts **Distributor** via the system
4. **Distributor** checks their inventory
5. If unavailable, **Distributor** contacts **Manufacturer**
6. **Manufacturer** provides production capacity and lead times
7. Information flows back through the chain
8. Order is fulfilled and tracked through the system

## User Types and Permissions

### Manufacturer
- Full access to all products, inventory, and orders
- Can manage production capacity and lead times
- Receives orders from distributors

### Distributor
- Access to assigned products and inventory
- Manages distribution center stock
- Receives orders from sellers, places orders with manufacturers

### Seller
- Access to retail inventory
- Creates customer orders
- Requests fulfillment from distributors

## Features

- **Real-time Inventory Tracking**: Monitor stock levels across all parties
- **Order Management**: Complete order lifecycle tracking
- **Multi-user Support**: Different interfaces for each user type
- **Service-Oriented Design**: Modular, scalable architecture
- **Modern UI**: Clean Windows Forms interface
- **Database Integration**: Persistent data storage with Entity Framework

## Future Enhancements

- Web-based client interface
- Real-time notifications
- Advanced reporting and analytics
- Mobile application support
- Integration with external systems
- Email notifications for order status changes

## Support

For technical support or questions about the implementation, please refer to the code documentation or contact the development team. 