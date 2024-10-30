using System;
using Core.Entities;

namespace Core.Interfaces;

public interface IProductRepository
{
    Task<IReadOnlyList<Product>> GetProductsAsync(String? brand, String? type, String? sort);
    Task<Product?> GetProductByIdAsync(int id);
    Task<IReadOnlyList<String>> GetTypesAsync();
    Task<IReadOnlyList<String>> GetBrandsAsync();
    void AddProduct(Product product);
    void UpdateProduct(Product product);
    void DeleteProduct(Product product);

    bool ProductExist(int id);
    Task<bool> SaveChangeAsync(); 
}
