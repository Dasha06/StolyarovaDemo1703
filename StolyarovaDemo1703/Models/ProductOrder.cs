using System;
using System.Collections.Generic;

namespace StolyarovaDemo1703.Models;

public partial class ProductOrder
{
    public int OrderId { get; set; }

    public string ProductArticul { get; set; } = null!;

    public int ProductOrderCount { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product ProductArticulNavigation { get; set; } = null!;
}
