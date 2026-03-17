using System;
using System.Collections.Generic;

namespace StolyarovaDemo1703.Models;

public partial class Address
{
    public int AddressId { get; set; }

    public string AddressName { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
