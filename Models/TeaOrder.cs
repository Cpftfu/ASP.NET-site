using System;
using System.Collections.Generic;

namespace TeaShop.Models;

public partial class TeaOrder
{
    public int IdTeaOrder { get; set; }

    public int Quantity { get; set; }

    public int OrderId { get; set; }

    public int IdTea { get; set; }

    public virtual Tea IdTeaNavigation { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
