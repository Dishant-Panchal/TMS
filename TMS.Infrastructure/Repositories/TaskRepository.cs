using Microsoft.EntityFrameworkCore;
using TMS.Domain.Models;

namespace TMS.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TMSContext _context;
        public TaskRepository(TMSContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmployeeTask>> GetAllAsync()
        {
            return await _context.EmployeeTasks.Include(t => t.Employee).ToListAsync();
        }

        public async Task<EmployeeTask?> GetByIdAsync(int id)
        {
            EmployeeTask? employeeTask = new();
            employeeTask = await _context.EmployeeTasks.FindAsync(id);
            if (employeeTask is not null)
            {
                employeeTask.Employee = await _context.Employees.FindAsync(employeeTask.EmployeeId);
            }
            return employeeTask;
        }

        public async Task AddAsync(EmployeeTask task)
        {
            await _context.EmployeeTasks.AddAsync(task);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(EmployeeTask task)
        {
            _context.EmployeeTasks.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var task = await _context.EmployeeTasks.FindAsync(id);
            if (task != null)
            {
                _context.EmployeeTasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }

        public async Task CompleteAsync(int id, bool isCmplt)
        {
            var task = await _context.EmployeeTasks.FindAsync(id);
            if (task != null)
            {
                task.IsCompleted = isCmplt;
                _context.EmployeeTasks.Update(task);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<EmployeeTask>> GetTeamTasksAsync(int managerId)
        {
            return await _context.EmployeeTasks.Where(x => x.Employee.ManagerId == managerId).Include(t => t.Employee).ToListAsync();
        }
    }

    public interface ITaskRepository
    {
        Task<IEnumerable<EmployeeTask>> GetAllAsync();
        Task<EmployeeTask?> GetByIdAsync(int id);
        Task AddAsync(EmployeeTask task);
        Task UpdateAsync(EmployeeTask task);
        Task DeleteAsync(int id);
        Task CompleteAsync(int id, bool isCmplt);
        Task<IEnumerable<EmployeeTask>> GetTeamTasksAsync(int managerId);
    }
}
