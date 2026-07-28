using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductRepository repo) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string? brand, 
    string? type, string? sort)
    {
        return Ok(await repo.GetProductsAsync(brand,type,sort));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await repo.GetProductByIdAsync(id);
        if(product == null) 
        {
            return NotFound();
        }
        return product;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        repo.AddProduct(product);
        if(await repo.SaveChangesAsync())
        {
            return CreatedAtAction("GetProduct", new {id = product.Id}, product);
        }
        return BadRequest("Probleam Creating the product.");
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProduct(int id, Product product)
    {
        if(product.Id !=id || !IsProductExists(id))
        {
            return BadRequest();
        }
        repo.UpdateProduct(product);
        if(await repo.SaveChangesAsync())
        {
            return NoContent();
        }

        return BadRequest("Probleam Updating the prduct");
    } 

    private bool IsProductExists(int id)
    {
        return repo.IsProductExists(id);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product = await repo.GetProductByIdAsync(id);
        
        if(product == null) return NotFound();
        repo.DeleteProduct(product);
           if(await repo.SaveChangesAsync())
        {
            return NoContent();
        }

        return BadRequest("Probleam in deleting the prduct");
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
    {
        return Ok(await repo.GetBrandAsync());
    }

    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
    {
        return Ok(await repo.GetTypeAsync());
    }
}