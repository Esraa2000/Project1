using System;
using System.Collections.Generic;

namespace COC.ModelDB.QUDB;

public partial class News
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }

    public DateTime Date { get; set; }

    public string? ImageUrl { get; set; }

    public bool? Highlight { get; set; }
}
