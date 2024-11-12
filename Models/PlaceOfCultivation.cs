using System;
using System.Collections.Generic;

namespace TeaShop.Models;

public partial class PlaceOfCultivation
{
    public int IdPlaceOfCultivation { get; set; }

    public string NameOfPlace { get; set; } = null!;

    public virtual ICollection<Tea> Teas { get; set; } = new List<Tea>();
}
