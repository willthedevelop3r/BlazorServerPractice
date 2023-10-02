using DataAccessLibrary.Models;

namespace DataAccessLibrary.Repository
{
    public interface IEmployeeRepository

    {
        Task<List<EmployeeModel>> GetEmployee();
        Task CreateEmployee(EmployeeModel employee);
        Task<EmployeeModel?> GetEmployeeById(int id);

        Task EditEmployee(EmployeeModel employee, int originalEmployeeId);

        Task DeleteEmployeeById(int id);
    }
}