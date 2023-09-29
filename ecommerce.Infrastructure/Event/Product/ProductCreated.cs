namespace ecommerce.Infrastructure.Event.Product;

public class ProductCreated
{
  public Guid ProductId { get; set; }
  public string ProductName { get; set; }
  public DateTime CreatedAt { get; set; }
}
