using System;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly MyStoreContext context;

    public ProductsController (MyStoreContext context){
        this.context = context;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts(){
        return await context.Products.ToListAsync();
    }
    [HttpGet("{id:int}")] // api/product/2
    public async Task<ActionResult<Product>> getProduct(int id){
        var product= await context.Products.FindAsync(id);
        if (product == null)
        return NotFound();
        return product;
    }
    [HttpPost]
    public async Task<ActionResult<Product>> createProduct(Product product){
        context.Products.Add(product);
        await context.SaveChangesAsync();
        return product;
    }
    [HttpPut ("{id:int}")]
    public async Task<ActionResult<Product>> updateProduct(int id, Product product){
        if (product.Id!=id || !ProductExist(id))
        return BadRequest("can not update this product!"); 
        context.Entry(product).State= EntityState.Modified;
        await context.SaveChangesAsync();
        return NoContent();
    }
    private bool ProductExist(int id){
        return context.Products.Any(x=>x.Id ==id);
    }
    [HttpDelete ("{id:int}")]
    public async Task<ActionResult<Product>> deleteProduct(int id){
        var product= await context.Products.FindAsync(id);
        if(product==null)
        return NotFound();
        context.Products.Remove(product);
        context.SaveChanges();
        return NoContent();
    }
}
