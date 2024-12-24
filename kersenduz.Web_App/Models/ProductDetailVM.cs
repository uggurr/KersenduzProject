namespace kersenduz.Web_App.Models
{
  public class ProductDetailVM
  {
    public int Id { get; set; }
    public string ProductName { get; set; }
    public string ProductCategory {  get; set; }
    public int ProductPrice { get; set; }
    public string ProductImg {  get; set; }

    public List<RawMetarials> RawMetarials { get; set; }
  }

  public class RawMetarials
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Unit { get; set; }
    public double Quantity { get; set; }
  }
}
