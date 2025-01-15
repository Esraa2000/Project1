using System;
using System.Collections.Generic;

namespace COC.ModelDB.QUDB;

public partial class MainMenu
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Order { get; set; }

    public virtual ICollection<SubMenu> SubMenus { get; set; } = new List<SubMenu>();
}
