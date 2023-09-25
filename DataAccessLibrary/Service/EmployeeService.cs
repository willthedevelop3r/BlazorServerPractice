using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ISqlDataAccess _db;

        public EmployeeService(ISqlDataAccess db)
        {
            _db = db;
        }

        public Task<List<EmployeeModel>> GetEmployee()
        {
            string sql = "SELECT * FROM dbo.Employee";

            return _db.LoadData<EmployeeModel, dynamic>(sql, new { });
        }

        public Task CreateEmployee(EmployeeModel employee)
        {
            string sql = @"INSERT INTO dbo.Employee (EmployeeId, FirstName, LastName, EmailAddress)
                            VALUES (@EmployeeId, @FirstName, @LastName, @EmailAddress);";

            return _db.SaveData(sql, employee);
        }

        public async Task<EmployeeModel?> GetEmployeeById(int id)
        {
            string sql = @"SELECT * FROM dbo.Employee WHERE EmployeeId = @Id";

            var param = new { Id = id };

            var results = await _db.LoadData<EmployeeModel, dynamic>(sql, param);
            return results.FirstOrDefault();
        }

        public async Task EditEmployee(EmployeeModel model, int originalEmployeeId)
        {
            var existingEmployee = await GetEmployeeById(model.EmployeeId);

            if (existingEmployee != null && existingEmployee.EmployeeId != originalEmployeeId)
            {
                throw new InvalidOperationException("Employee with this ID already exists");
            }

            string sql = @"UPDATE dbo.Employee 
                   SET 
                       EmployeeId = @EmployeeId,
                       FirstName = @FirstName, 
                       LastName = @LastName, 
                       EmailAddress = @EmailAddress 
                   WHERE EmployeeId = @OriginalEmployeeId";

            var parameters = new
            {
                EmployeeId = model.EmployeeId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                EmailAddress = model.EmailAddress,
                OriginalEmployeeId = originalEmployeeId
            };

            await _db.SaveData(sql, parameters);
        }

        public Task DeleteEmployeeById(int id)
        {
            string sql = @"DELETE FROM dbo.Employee WHERE EmployeeId = @Id";

            var param = new { Id = id };

            return _db.SaveData(sql, param);
        }
    }
}
