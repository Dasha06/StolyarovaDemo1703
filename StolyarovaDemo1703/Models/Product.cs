using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;

namespace StolyarovaDemo1703.Models;

public partial class Product
{
    public string ProductArticul { get; set; } = null!;

    public double ProductPrice { get; set; }

    public int ProductActiveDiscount { get; set; }

    public int ProductCount { get; set; }

    public string ProductDescription { get; set; } = null!;

    public string? ProductImage { get; set; }

    public string ProductImageWithDefault
    {
        get
        {
            if (ProductImage != null && ProductImage != "") return "/images/" + ProductImage;
            else return "/images/picture.png";
        }
    }

    public string BackgroundColor
    {
        get
        {
            if (ProductActiveDiscount > 14) return "#2E8B57";
            else if (ProductCount == 0) return "Blue";
            else return "";
        }
    }

    public int ProdNameId { get; set; }

    public int UnitId { get; set; }

    public int ProviderId { get; set; }

    public int ManufacturerId { get; set; }

    public int CategoryId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Manufacturer Manufacturer { get; set; } = null!;

    public virtual ProductName ProdName { get; set; } = null!;

    public virtual ICollection<ProductOrder> ProductOrders { get; set; } = new List<ProductOrder>();

    public virtual Provider Provider { get; set; } = null!;

    public virtual Unit Unit { get; set; } = null!;
}
