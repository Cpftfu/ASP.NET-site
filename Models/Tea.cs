using System;
using System.Collections.Generic;

namespace TeaShop.Models;

public partial class Tea
{
    public int IdTea { get; set; }

    public string TeaName { get; set; } = null!;

    public decimal PriceOfTea { get; set; }

    public int CategoryId { get; set; }

    public int PlaceOfCultivationId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual PlaceOfCultivation PlaceOfCultivation { get; set; } = null!;

    public virtual ICollection<TeaOrder> TeaOrders { get; set; } = new List<TeaOrder>();
}
