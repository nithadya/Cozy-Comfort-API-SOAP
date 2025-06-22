using System.Net.Http;
using System.Text;
using System.Text.Json;
using CozyComfort.WindowsFormsClient.Models;

namespace CozyComfort.WindowsFormsClient.Services
{
    public class ServiceClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ServiceClient()
        {
            _httpClient = new HttpClient();
            _baseUrl = "https://localhost:7001/api"; // Update this to your API URL
        }

        // User Service Methods
        public async Task<User?> AuthenticateUserAsync(string username, string password, UserType userType)
        {
            try
            {
                var url = $"{_baseUrl}/User/authenticate?username={username}&password={password}&userType={userType}";
                var response = await _httpClient.PostAsync(url, null);
                
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<User>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/User");
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<User>>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<User>();
                }
            }
            catch { }
            return new List<User>();
        }

        public async Task<User?> CreateUserAsync(User user)
        {
            try
            {
                var json = JsonSerializer.Serialize(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{_baseUrl}/User", content);
                
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<User>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
            }
            catch { }
            return null;
        }

        // Product Service Methods
        public async Task<List<Product>> GetAllProductsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/Product");
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<Product>>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<Product>();
                }
            }
            catch { }
            return new List<Product>();
        }

        public async Task<Product?> CreateProductAsync(Product product)
        {
            try
            {
                var json = JsonSerializer.Serialize(product);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{_baseUrl}/Product", content);
                
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<Product>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
            }
            catch { }
            return null;
        }

        public async Task<List<Product>> SearchProductsAsync(string? name = null, string? category = null, decimal? minPrice = null, decimal? maxPrice = null)
        {
            try
            {
                var queryParams = new List<string>();
                if (!string.IsNullOrEmpty(name)) queryParams.Add($"name={Uri.EscapeDataString(name)}");
                if (!string.IsNullOrEmpty(category)) queryParams.Add($"category={Uri.EscapeDataString(category)}");
                if (minPrice.HasValue) queryParams.Add($"minPrice={minPrice}");
                if (maxPrice.HasValue) queryParams.Add($"maxPrice={maxPrice}");

                var queryString = queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : "";
                var response = await _httpClient.GetAsync($"{_baseUrl}/Product/search{queryString}");
                
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<Product>>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<Product>();
                }
            }
            catch { }
            return new List<Product>();
        }

        // Inventory Service Methods
        public async Task<List<Inventory>> GetAllInventoriesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/Inventory");
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<Inventory>>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<Inventory>();
                }
            }
            catch { }
            return new List<Inventory>();
        }

        public async Task<List<Inventory>> GetInventoryByOwnerAsync(int ownerId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/Inventory/owner/{ownerId}");
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<Inventory>>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<Inventory>();
                }
            }
            catch { }
            return new List<Inventory>();
        }

        public async Task<bool> CheckAvailabilityAsync(int productId, int ownerId, int requiredQuantity)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/Inventory/check-availability/{productId}/{ownerId}?requiredQuantity={requiredQuantity}");
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<bool>(jsonString);
                }
            }
            catch { }
            return false;
        }

        // Order Service Methods
        public async Task<List<Order>> GetAllOrdersAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/Order");
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<Order>>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<Order>();
                }
            }
            catch { }
            return new List<Order>();
        }

        public async Task<Order?> CreateOrderAsync(Order order)
        {
            try
            {
                var json = JsonSerializer.Serialize(order);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{_baseUrl}/Order", content);
                
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<Order>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
            }
            catch { }
            return null;
        }

        public async Task<List<Order>> GetOrdersByCustomerAsync(int customerId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/Order/customer/{customerId}");
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<Order>>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<Order>();
                }
            }
            catch { }
            return new List<Order>();
        }

        public async Task<bool> UpdateOrderStatusAsync(int orderId, OrderStatus status)
        {
            try
            {
                var json = JsonSerializer.Serialize(status);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"{_baseUrl}/Order/{orderId}/status", content);
                return response.IsSuccessStatusCode;
            }
            catch { }
            return false;
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}