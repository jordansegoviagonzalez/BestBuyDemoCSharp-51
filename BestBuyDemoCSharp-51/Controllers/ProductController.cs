using BestBuyDemoCSharp_51.Data;
using BestBuyDemoCSharp_51.Models;
using Microsoft.AspNetCore.Mvc;

namespace BestBuyDemoCSharp_51.Controllers;
/*ProductController: Entry point for HTTP requests related to products.
 * 
 * - Function: Handles user actions such as “show me all products.”
 * - Coordination: Bridges between the web interface (views) and data access layer (repository).
 * - Responsibility: Receives requests, fetches data via the repository, and returns the appropriate view with data.
 */


public class ProductController : Controller 
{
    private readonly IProductRepository _repository;

    public ProductController(IProductRepository repository)
    {
        _repository = repository;
    }
    // GET
    /*method to get all the products and sends them to the view.
     *Uses dependency-injected repository to execute a DB query,
     * returning strongly typed data, passed to the view as a model structure, declared as Product.cs
     */
    public IActionResult Index()
    {
        var products = _repository.GetAllProducts();
        return View(products);
    }
   
    public IActionResult ViewProduct(int id)
    {
        var product = _repository.GetProduct(id);
        return View(product);
    }
    
    public IActionResult UpdateProduct(int id)
    {
        var product = _repository.GetProduct(id);
        if (product == null)
        {
            return View("ProductNotFound");
        }
        return View(product);
    }
    
    public IActionResult UpdateProductToDatabase(Product product)
    {
        _repository.UpdateProduct(product);

        return RedirectToAction("ViewProduct",new { id = product.ProductID });
    }
    
    public IActionResult InsertProduct()
    {
        var prod = _repository.AssignCategory();
        return View(prod);
    }
    
    public IActionResult InsertProductToDatabase(Product productToInsert)
    {
        _repository.InsertProduct(productToInsert);
        return RedirectToAction("Index");
    }
    
    public IActionResult DeleteProduct(Product product)
    {
        _repository.DeleteProduct(product);
        return RedirectToAction("Index");
    }
    
    
    
}