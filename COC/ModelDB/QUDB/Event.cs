using System;
using System.Collections.Generic;

namespace COC.ModelDB.QUDB;

public partial class Event
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }
}
