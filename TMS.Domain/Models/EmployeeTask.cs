using System;
using System.Collections.Generic;

namespace TMS.Domain.Models;

public partial class EmployeeTask
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateTime? DueDate { get; set; }

    public bool? IsCompleted { get; set; }

    public int? EmployeeId { get; set; }

    public DateTime? CreatedDateTime { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual ICollection<TaskAttachment> TaskAttachments { get; set; } = new List<TaskAttachment>();

    public virtual ICollection<TaskNote> TaskNotes { get; set; } = new List<TaskNote>();
}
