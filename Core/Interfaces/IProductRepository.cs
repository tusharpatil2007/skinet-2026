using Core.Entities;

public interface IProductRepository
{
    Task<IReadOnlyList<Product>> GetProductsAsync(string? brands, string? type, string? sort);

    Task<Product?> GetProductByIdAsync(int id);

    Task<IReadOnlyList<string>> GetBrandAsync();

    Task<IReadOnlyList<string>> GetTypeAsync();

    void AddProduct(Product product);

    void UpdateProduct(Product product);

    void DeleteProduct(Product product);

    bool IsProductExists(int id);

    Task<bool> SaveChangesAsync();

}