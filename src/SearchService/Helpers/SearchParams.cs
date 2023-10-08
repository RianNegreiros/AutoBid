namespace SearchService.Helpers;

public class SearchParams
{
  public string Parameter { get; set; }
  public int PageNumber { get; set; } = 1;
  public int PageSize { get; set; } = 10;
  public string Winner { get; set; }
  public string Seller { get; set; }
  public string OrderBy { get; set; }
  public string FilterBy { get; set; }
}
