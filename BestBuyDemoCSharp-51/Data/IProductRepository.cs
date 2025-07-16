using BestBuyDemoCSharp_51.Models;
using System.Collections.Generic;
using System;
namespace BestBuyDemoCSharp_51.Data;

/* Interface = Job contract for the class ProductRepository, it engoforcers what the class MUST implement
 * tells the class exaclty what it must do.
 */


public interface IProductRepository //declares required method
{
    public IEnumerable<Product> GetAllProducts(); 
    Product GetProduct(int id);
    public void UpdateProduct(Product product);
    public void InsertProduct(Product productToInsert);
    public IEnumerable<Category> GetCategories();
    public Product AssignCategory();
    
    public void DeleteProduct(Product product);
} 