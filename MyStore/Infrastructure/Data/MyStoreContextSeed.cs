using System;
using System.Text.Json;
using Core.Entities;

namespace Infrastructure.Data;

public class MyStoreContextSeed
{
    public static async Task SeedAsync(MyStoreContext context){
        var productsData= await File.ReadAllTextAsync("../Infrastructure/Data/Seedata/products.json");
        var products = JsonSerializer.Deserialize<List<Product>> (productsData);
        if (products == null)
        return;
        context.Products.AddRange(products);
        await context.SaveChangesAsync();
    }
}
