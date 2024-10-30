using System;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductRepository repo) : ControllerBase
{
  
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(String? brand, String? type, String? sort){
        return Ok (await repo.GetProductsAsync(brand, type, sort));
    }
    [HttpGet("{id:int}")] // api/product/2
    public async Task<ActionResult<Product>> getProduct(int id){
        var product= await repo.GetProductByIdAsync(id);
        if (product == null)
        return NotFound();
        return product;
    }
    [HttpPost]
    public async Task<ActionResult<Product>> createProduct(Product product){
        repo.AddProduct(product);
        if(await repo.SaveChangeAsync()){
           return  CreatedAtAction("GetProducts", new {id=product.Id, product});
        }
        return BadRequest("cannot add product");

    }
    [HttpPut ("{id:int}")]
    public async Task<ActionResult<Product>> updateProduct(int id, Product product){
        if (product.Id!=id || !ProductExist(id))
        return BadRequest("can not update this product!"); 
        repo.UpdateProduct(product);
        await repo.SaveChangeAsync();
        return NoContent();
    }
    private bool ProductExist(int id){
        return repo.ProductExist(id);
    }
    [   HttpDelete ("{id:int}")]
    public async Task<ActionResult<Product>> deleteProduct(int id){
        var product= await repo.GetProductByIdAsync(id);
        if(product==null)
        return NotFound();
        repo.DeleteProduct(product);
        repo.SaveChangeAsync();
        return NoContent();
    }
    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<String>>> getTypes(){
        return Ok(await repo.GetTypesAsync());
    }
    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<String>>> getBrands(){
        return Ok(await repo.GetBrandsAsync());
    }
}
