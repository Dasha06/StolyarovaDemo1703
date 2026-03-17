using System;
using System.Collections.Generic;

namespace StolyarovaDemo1703.Models;

public partial class ProductName
{
    public int ProdNameId { get; set; }

    public string ProdName { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
