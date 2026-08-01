using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
[ApiController]
[Route("api/[controller]")]
public class ProductsController(IgenericRepository<Product> repo) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(
        [FromQuery]ProductSpecParams specParams)
    {
        var spec = new ProductSpecification(specParams);
        return await CreatePagedResult(repo,spec,specParams.PageIndex,specParams.PageSize);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await repo.GetByIdAsync(id);
        //var product = await repo.GetProductByIdAsync(id);
        if(product == null) 
        {
            return NotFound();
        }
        return product;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        repo.Add(product);
        if(await repo.SaveAllAsync())
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
        repo.Update(product);
        if(await repo.SaveAllAsync())
        {
            return NoContent();
        }

        return BadRequest("Probleam Updating the prduct");
    } 

    private bool IsProductExists(int id)
    {
        return repo.Exits(id);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product = await repo.GetByIdAsync(id);
        
        if(product == null) return NotFound();
        repo.Remove(product);
           if(await repo.SaveAllAsync())
        {
            return NoContent();
        }

        return BadRequest("Probleam in deleting the prduct");
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
    {
        var spec = new BrandListSpecification();
        return Ok(await repo.ListAsync(spec));
        //return Ok(await repo.GetBrandAsync());
    }

    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
    {
        var spec = new TypeListSpecification();
        return Ok(await repo.ListAsync(spec));
        //return Ok(await repo.GetTypeAsync());
    }
}