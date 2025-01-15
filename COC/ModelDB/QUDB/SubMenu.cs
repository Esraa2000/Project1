using System;
using System.Collections.Generic;

namespace COC.ModelDB.QUDB;

public partial class SubMenu
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Order { get; set; }

    public string? Content { get; set; }

    public string? Url { get; set; }

    public string? Title { get; set; }

    public int? MainMenuId { get; set; }

    public virtual MainMenu? MainMenu { get; set; }
}
