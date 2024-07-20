using System;
using System.Collections.Generic;

namespace TMS.Domain.Models;

public partial class TaskAttachment
{
    public int Id { get; set; }

    public int? TaskId { get; set; }

    public string? FileName { get; set; }

    public string? FileExtension { get; set; }

    public DateTime? CreatedDateTime { get; set; }

    public virtual EmployeeTask? Task { get; set; }
}
