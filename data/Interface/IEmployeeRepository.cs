using data.Models;
using data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace data.Interface
{
    public interface IEmployeeRepository
    {
        public string addEmployee(Employee employee);
        public List<EmployeeSummary> getEmployees();
        public string updateEmployee(Employee employee);
        public string deleteEmployee(int employeeId);
        public Employee getEmployee(int employeeId);
        public bool verifyEmail(EmployeeEmailData employeeData);
    }
}
