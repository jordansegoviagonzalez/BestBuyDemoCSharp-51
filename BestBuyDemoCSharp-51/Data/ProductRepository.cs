using Dapper;
using System.Data;
using BestBuyDemoCSharp_51.Models;

/* Class in Data directory = The worker executing the contract
 * provides the actual; implementation of the  interfaces methods
 * ensures the job gets done exactly as specified by the  contract
 */

namespace BestBuyDemoCSharp_51.Data;

public class ProductRepository : IProductRepository

{
    private readonly IDbConnection _connection;
    /*Field: _connection
     * holds a reference to a database connection, abstracted by the interface IDbConnection
     * readonly means it can only be assigned once in the constructor preventing accidental reassignment
     * 
     */
    
    
    /* use the QuerySingle Dapper method so that we can return a single row
   Creating the GetProduct() method implementation in the ProductRepository class
 */    

    public ProductRepository(IDbConnection connection)
    /* When you create a ProductRepository instance, a database connection is injected.
     * The parameter 'connection' is of type IDbConnection, an interface that represents a connection to a database.
     * This abstraction allows the repository to work seamlessly with various database systems 
     * (SQL Server, PostgreSQL, MySQL) without being tied to a specific provider aka database
     */
    {
        _connection = connection;
        
    }
    
    // Retrieves all product records from the database table 'products'
    public IEnumerable<Product> GetAllProducts()
    {
        return _connection.Query<Product>("SELECT * FROM products");
    }

    // Gets one product by its ID  
    public Product GetProduct(int id)
    {
        return _connection.QuerySingle<Product>("SELECT * FROM PRODUCTS WHERE PRODUCTID = @id", new { id = id });
    }
   // Updates a product's name and price by its ID
    public void UpdateProduct(Product product)
    {
        _connection.Execute("UPDATE products SET Name = @name, Price = @price WHERE ProductID = @id",
            new {name = product.Name, price = product.Price, id = product.ProductID });
    }

    public void InsertProduct(Product productToInsert)
    {
        _connection.Execute("INSERT INTO products (NAME, PRICE, CATEGORYID) VALUES (@name, @price, @categoryID);",
            new {name = productToInsert.Name, price = productToInsert.Price, categoryID = productToInsert.CategoryID });
    }

    public IEnumerable<Category> GetCategories()
    {
        return _connection.Query<Category>("SELECT * FROM categories;");
    }

    public Product AssignCategory()
    {
        var categoryList = GetCategories();
        var product = new Product();
        product.Categories = categoryList;
        return product;
    }

    public void DeleteProduct(Product product)
    {
        _connection.Execute("DELETE FROM REVIEWS WHERE ProductID = @id;", new {id = product.ProductID });
        _connection.Execute("DELETE FROM Sales WHERE ProductID = @id;",  new { id = product.ProductID });
        _connection.Execute("DELETE FROM Products WHERE ProductID = @id;", new { id = product.ProductID });
    }
}