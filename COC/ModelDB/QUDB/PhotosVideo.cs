using System;
using System.Collections.Generic;

namespace COC.ModelDB.QUDB;

public partial class PhotosVideo
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? ImageUrl { get; set; }

    public string? VideoUrl { get; set; }
}
