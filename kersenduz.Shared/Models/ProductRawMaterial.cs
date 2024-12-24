using kersenduz.Shared.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kersenduz.Shared.Models
{
  public class ProductRawMaterial:BaseModel
  {
    public int ProductId { get; set; }
    public int RawMaterialId { get; set; }
  }
}
