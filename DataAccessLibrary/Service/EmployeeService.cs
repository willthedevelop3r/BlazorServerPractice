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
            string sql = "select * from dbo.Employee";

            return _db.LoadData<EmployeeModel, dynamic>(sql, new { });
        }

        public Task CreateEmployee(EmployeeModel employee)
        {
            string sql = @"insert into dbo.Employee (EmployeeId, FirstName, LastName, EmailAddress)
                            values (@EmployeeId, @FirstName, @LastName, @EmailAddress);";

            return _db.SaveData(sql, employee);
        }

        public async Task<EmployeeModel?> GetEmployeeById(int id)
        {
            string sql = @"SELECT * FROM dbo.Employee WHERE EmployeeId = @Id";

            var param = new { Id = id };

            var results = await _db.LoadData<EmployeeModel, dynamic>(sql, param);
            return results.FirstOrDefault();
        }
    }
}
