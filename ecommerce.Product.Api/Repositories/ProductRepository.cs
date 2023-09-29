using ecommerce.Infrastructure.Command.Product;
using ecommerce.Infrastructure.Event.Product;
using MongoDB.Driver;

namespace ecommerce.Product.Api.Repositories;

public class ProductRepository : IProductRepository
{
  private readonly IMongoDatabase _database;
  private readonly IMongoCollection<CreateProduct> _collection;

  public ProductRepository(IMongoDatabase database)
  {
    _database = database;
    _collection = _database.GetCollection<CreateProduct>("product");
  }

  public async Task<ProductCreated> AddProduct(CreateProduct product)
  {
    await _collection.InsertOneAsync(product);
    return new ProductCreated { ProductId = product.ProductId, ProductName = product.ProductName, CreatedAt = DateTime.UtcNow };
  }

  public async Task<ProductCreated> GetProduct(string ProductId)
  {
    var product = _collection.AsQueryable().Where(x => x.ProductId == ProductId).FirstOrDefault() ?? throw new Exception("Product not found");

    await Task.CompletedTask;
    return new ProductCreated { ProductId = product.ProductId, ProductName = product.ProductName, CreatedAt = DateTime.UtcNow };
  }
}
