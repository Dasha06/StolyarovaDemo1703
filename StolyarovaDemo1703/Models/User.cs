using System;
using System.Collections.Generic;

namespace StolyarovaDemo1703.Models;

public partial class User
{
    public int UserId { get; set; }

    public string UserFio { get; set; } = null!;

    public int RoleId { get; set; }

    public string UserLogin { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Role Role { get; set; } = null!;
}
