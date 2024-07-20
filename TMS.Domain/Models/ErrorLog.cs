using System;
using System.Collections.Generic;

namespace TMS.Domain.Models;

public partial class ErrorLog
{
    public int Id { get; set; }

    public string? Exception { get; set; }

    public string? StackTrace { get; set; }

    public string? ErrorMessage { get; set; }

    public DateTime? CreatedDateTime { get; set; }
}
