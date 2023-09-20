using DataAccessLibrary.Models;

namespace DataAccessLibrary.Service
{
    public interface IEmployeeService

    {
        Task<List<EmployeeModel>> GetEmployee();
        Task CreateEmployee(EmployeeModel employee);
        Task<EmployeeModel?> GetEmployeeById(int id);
    }
}