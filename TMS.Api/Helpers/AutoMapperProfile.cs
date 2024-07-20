using AutoMapper;
using TMS.Domain.Models;
using TMS.Domain.ViewModels;

namespace TMS.Api.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<TaskVM, EmployeeTask>();
            CreateMap<EmployeeTask, TaskVM>();
        }
    }
}
