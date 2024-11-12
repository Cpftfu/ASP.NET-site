using System;
using System.Collections.Generic;

namespace TeaShop.Models;

public partial class Customer
{
    public int IdCustomer { get; set; }

    public string NameOfCust { get; set; } = null!;

    public string SurnameOfCust { get; set; } = null!;

    public string MiddlenameOfCust { get; set; } = null!;

    public string EmailOfCust { get; set; } = null!;

    public string LoginOfCust { get; set; } = null!;

    public string PasswordOfCust { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
