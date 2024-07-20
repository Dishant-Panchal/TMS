using System;
using System.Collections.Generic;

namespace TMS.Domain.Models;

public partial class TaskNote
{
    public int Id { get; set; }

    public int? TaskId { get; set; }

    public string? Note { get; set; }
}
