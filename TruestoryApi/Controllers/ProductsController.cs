/*
Code by: Paulo Eder Medeiros Cardoso
*/

using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Text.Json;
using TruestoryApi.Models;

namespace TruestoryApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl = "https://api.restful-api.dev/objects";

    public ProductsController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    /// <summary>
    /// Retrieves products from the mock API with optional name filtering and pagination.
    /// </summary>
    /// <param name="name">Substring to search in product names</param>
    /// <param name="page">Page number (default is 1)</param>
    /// <param name="pageSize">Number of results per page (default is 5)</param>
    /// <returns>A list of filtered and paginated products</returns>
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string? name, int page = 1, int pageSize = 5)
    {
        try
        {
            // Fetch all objects from the mock API
            var response = await _httpClient.GetFromJsonAsync<JsonElement>(_baseUrl);
            var allObjects = response.EnumerateArray();

            // Filter by name (if provided), then paginate
            var filtered = allObjects
                .Where(obj =>
                    obj.TryGetProperty("name", out var nameProp) &&
                    (name == null || nameProp.GetString()!.ToLower().Contains(name.ToLower()))
                )
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Ok(filtered);
        }
        catch (Exception ex)
        {
            // Handle unexpected errors
            return StatusCode(500, $"Internal error: {ex.Message}");
        }
    }

    /// <summary>
    /// Creates a new product by sending it to the mock API.
    /// </summary>
    /// <param name="product">The product to be added</param>
    /// <returns>The created product response</returns>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Product product)
    {
        // Validate input model
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            // Send the new product to the mock API
            var response = await _httpClient.PostAsJsonAsync(_baseUrl, new
            {
                name = product.Name,
                data = product.Data
            });

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());

            var result = await response.Content.ReadFromJsonAsync<dynamic>();
            return Ok(result);
        }
        catch (Exception ex)
        {
            // Handle unexpected errors
            return StatusCode(500, $"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Deletes a product from the mock API using its ID.
    /// </summary>
    /// <param name="id">The ID of the product to delete</param>
    /// <returns>Status message</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            // Send DELETE request to mock API
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());

            return Ok("Deleted");
        }
        catch (Exception ex)
        {
            // Handle unexpected errors
            return StatusCode(500, $"Error: {ex.Message}");
        }
    }
}
