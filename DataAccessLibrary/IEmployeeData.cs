using DataAccessLibrary.Models;

namespace DataAccessLibrary
{
    public interface IEmployeeData
    {
        Task<List<EmployeeModel>> GetEmployee();
        Task InsertPerson(EmployeeModel employee);
    }
}