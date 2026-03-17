using System;
using System.Collections.Generic;

namespace StolyarovaDemo1703.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public DateOnly OrderCreateDate { get; set; }

    public DateOnly OrderDeliveryDate { get; set; }

    public int UserId { get; set; }

    public int AddressId { get; set; }

    public int OrderCode { get; set; }

    public int StatusId { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual ICollection<ProductOrder> ProductOrders { get; set; } = new List<ProductOrder>();

    public virtual Status Status { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
