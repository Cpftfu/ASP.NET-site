using System;
using System.Collections.Generic;

namespace TeaShop.Models;

public partial class Order
{
    public int IdOrder { get; set; }

    public DateTime OrderDate { get; set; }

    public decimal TotalAmount { get; set; }

    public int CustomerId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<TeaOrder> TeaOrders { get; set; } = new List<TeaOrder>();
}
