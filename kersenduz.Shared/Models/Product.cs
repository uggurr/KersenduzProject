using kersenduz.Shared.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kersenduz.Shared.Models
{
  public class Product:BaseModel
  {
    public string Name { get; set; }
    public int CategoryId { get; set; }
    public string Img { get; set; }
    public int CostPrice { get; set; }
  }
}
