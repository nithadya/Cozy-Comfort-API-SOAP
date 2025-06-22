# Cozy Comfort - SOC-Based Blanket Manufacturing Solution

A Service-Oriented Computing (SOC) solution for streamlining ordering processes between Manufacturers, Distributors, and Sellers in the blanket manufacturing industry.

## Project Structure

The solution has been **consolidated** into 2 main projects:

### 1. CozyComfort.API (.NET 8.0 Web API)
- **Location**: `CozyComfort.API/`
- **Purpose**: Backend API service with consolidated models and data access
- **Contains**:
  - `/Models/` - All data models (User, Product, Order, Inventory)
  - `/Data/` - Entity Framework DbContext and database configuration
  - `/Controllers/` - Web API controllers (UserController, ProductController, InventoryController, OrderController)
  - Entity Framework Code First with SQL Server LocalDB

### 2. CozyComfort.WindowsFormsClient (.NET 8.0 Windows Forms)
- **Location**: `CozyComfort.WindowsFormsClient/`
- **Purpose**: Desktop client application
- **Contains**:
  - `/Models/` - Client-side model definitions
  - `/Forms/` - Windows Forms (LoginForm, MainForm)
  - `/Services/` - ServiceClient for API communication
  - HTTP client for consuming Web API services

## Key Features

### User Management
- **User Types**: Manufacturer, Distributor, Seller
- **Authentication**: Role-based login system
- **User Data**: Company information, contact details, activity status

### Product Catalog
- **Product Information**: Name, SKU, description, material, size, color
- **Pricing**: Unit prices with production capacity tracking
- **Lead Times**: Production timeline management
- **Search & Filter**: Advanced product search capabilities

### Inventory Management
- **Stock Tracking**: Real-time inventory levels per user/product
- **Reorder Levels**: Automated low-stock alerts
- **Location Tracking**: Warehouse/store location management
- **Availability Checks**: Real-time stock availability verification

### Order Processing
- **Order Lifecycle**: Complete order management from creation to delivery
- **Status Tracking**: 9-stage order status system
- **Multi-Party Orders**: Seller → Distributor → Manufacturer workflow
- **Order Items**: Detailed line items with pricing

## Technology Stack

- **.NET 8.0**: Latest .NET framework
- **ASP.NET Core Web API**: RESTful API services
- **Entity Framework Core**: Code-First database approach
- **SQL Server LocalDB**: Development database
- **Windows Forms**: Desktop client interface
- **HTTP Client**: API communication
- **JSON Serialization**: Data exchange format

## Database Schema

### Tables Created:
1. **Users**: User accounts and company information
2. **Products**: Blanket product catalog
3. **Inventories**: Stock levels per user/product
4. **Orders**: Order headers with customer info
5. **OrderItems**: Detailed order line items

### Seeded Data:
- 3 Users (1 Manufacturer, 1 Distributor, 1 Seller)
- 3 Products (Luxury Wool, Cotton Comfort, Fleece Throw)
- 9 Inventory records (3 products × 3 users)

## API Endpoints

### User Service (`/api/User`)
- `POST /authenticate` - User authentication
- `GET /` - Get all users
- `GET /{id}` - Get user by ID
- `POST /` - Create new user
- `PUT /{id}` - Update user
- `DELETE /{id}` - Delete user
- `GET /bytype/{userType}` - Get users by type

### Product Service (`/api/Product`)
- `GET /` - Get all products
- `GET /{id}` - Get product by ID
- `POST /` - Create new product
- `PUT /{id}` - Update product
- `DELETE /{id}` - Delete product
- `GET /search` - Search products with filters
- `GET /category/{category}` - Get products by category
- `GET /categories` - Get all categories

### Inventory Service (`/api/Inventory`)
- `GET /` - Get all inventories
- `GET /{id}` - Get inventory by ID
- `POST /` - Create new inventory
- `PUT /{id}` - Update inventory
- `GET /product/{productId}` - Get inventory by product
- `GET /owner/{ownerId}` - Get inventory by owner
- `GET /check-availability/{productId}/{ownerId}` - Check availability
- `POST /update-stock` - Update stock levels
- `GET /low-stock` - Get low stock items

### Order Service (`/api/Order`)
- `GET /` - Get all orders
- `GET /{id}` - Get order by ID
- `POST /` - Create new order
- `PUT /{id}` - Update order
- `DELETE /{id}` - Delete order
- `GET /customer/{customerId}` - Get orders by customer
- `GET /status/{status}` - Get orders by status
- `PUT /{id}/status` - Update order status
- `POST /{id}/assign` - Assign order to user

## Getting Started

### Prerequisites
- Visual Studio 2022 or later
- .NET 8.0 SDK
- SQL Server LocalDB

### Running the Application

1. **Start the API Server**:
   ```bash
   cd CozyComfort.API
   dotnet run
   ```
   API will be available at: `https://localhost:7001`

2. **Start the Windows Forms Client**:
   ```bash
   cd CozyComfort.WindowsFormsClient
   dotnet run
   ```

3. **Database Setup**:
   - Database is automatically created on first run
   - Uses Entity Framework Code First approach
   - Seeded with sample data

## Order Workflow

1. **Seller** creates order for customer
2. **System** checks inventory availability
3. **Distributor** confirms stock or requests from manufacturer
4. **Manufacturer** produces items if needed
5. **Order** progresses through status stages:
   - Pending → CheckingInventory → ConfirmedWithDistributor
   - ConfirmedWithManufacturer → InProduction → ReadyForShipment
   - Shipped → Delivered

## Architecture Benefits

- **Consolidated Structure**: Simplified deployment and maintenance
- **RESTful APIs**: Modern, scalable web services
- **Entity Framework**: Code-first database management
- **Role-Based Access**: Secure user management
- **Real-Time Data**: Live inventory and order tracking
- **Extensible Design**: Easy to add new features

## Development Notes

- **Connection String**: Configured in `appsettings.json`
- **CORS**: Enabled for client-server communication
- **Swagger**: API documentation available in development mode
- **Error Handling**: Comprehensive exception management
- **Data Validation**: Model validation attributes

## Future Enhancements

- Web-based client interface
- Advanced reporting and analytics
- Email notifications for order status
- Integration with external shipping providers
- Mobile application support

## License

This project is developed for educational purposes as part of a Service-Oriented Computing (SOC) solution demonstration. 