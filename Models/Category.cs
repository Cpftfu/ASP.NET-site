using System;
using System.Collections.Generic;

namespace TeaShop.Models;

public partial class Category
{
    public int IdCategory { get; set; }

    public string NameOfCategory { get; set; } = null!;

    public virtual ICollection<Tea> Teas { get; set; } = new List<Tea>();
}
