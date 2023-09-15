using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class EmployeeData : IEmployeeData
    {
        private readonly ISqlDataAccess _db;

        public EmployeeData(ISqlDataAccess db)
        {
            _db = db;
        }

        public Task<List<EmployeeModel>> GetEmployee()
        {
            string sql = "select * from dbo.Employee";

            return _db.LoadData<EmployeeModel, dynamic>(sql, new { });
        }

        public Task InsertEmployee(EmployeeModel employee)
        {
            string sql = @"insert into dbo.Employee (EmployeeId, FirstName, LastName, EmailAddress)
                            values (@EmployeeId, @FirstName, @LastName, @EmailAddress);";

            return _db.SaveData(sql, employee);
        }
    }
}
