using kersenduz.Shared.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kersenduz.Shared.Models
{
  public class RawMaterial:BaseModel
  {
    public string Name { get; set; }
    public double Price { get; set; }
    public int UnitId { get; set; }
  }
}
