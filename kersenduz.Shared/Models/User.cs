﻿using kersenduz.Shared.Models.BaseModels;

namespace kersenduz.Shared.Models;

public class User : BaseModel
{
    
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string UserName { get; set; }
}
