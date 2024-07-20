using Microsoft.AspNetCore.Http;

namespace TMS.Domain.ViewModels
{
    public class TaskVM
    {
        public int? Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public int? EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public string? Notes { get; set; }
        public IFormFile? Attachment { get; set; }
        public string? File { get; set; }
    }
}
