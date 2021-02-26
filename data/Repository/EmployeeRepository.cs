using data.Context;
using data.Interface;
using data.Models;
using data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace data.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DataContext _context;
        public EmployeeRepository(DataContext context)
        {
            _context = context;
        }
        public string addEmployee(Employee employee)
        {
            try
            {
                employee.CreatedDate = DateTime.Now.ToString();
                _context.Employees.Add(employee);
                _context.SaveChanges();

                return "Employee Added Successfully";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string updateEmployee(Employee employee)
        {
            try
            {
                Employee employeeData = _context.Employees.Where(u => u.Id == employee.Id).FirstOrDefault();
                if (employeeData != null)
                {
                    employeeData.Name = employee.Name;
                    employeeData.Email = employee.Email;
                    employeeData.Gender = employee.Gender;
                    employeeData.Designation = employee.Designation;
                    employeeData.Status = employee.Status;
                    employeeData.Mobile = employee.Mobile;
                    employeeData.Bca = employee.Bca;
                    employeeData.Bsc = employee.Bsc;
                    employeeData.Mca = employee.Mca;
                    employeeData.ImageId = employee.ImageId;
                    _context.Employees.Update(employee);
                    _context.SaveChanges();
                }
                else
                {
                    return "Invalid Employee";
                }


                return "Employee Added Successfully";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string deleteEmployee(int employeeId)
        {
            try
            {
                Employee employee = _context.Employees.Where(u => u.Id == employeeId).FirstOrDefault();
                if (employee != null)
                {
                    _context.Employees.Remove(employee);
                    _context.SaveChanges();
                }
                else
                {
                    return "Invalid Employee Id";
                }
                return "Employee Deleted Successfully";

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Employee getEmployee(int employeeId)
        {
            try
            {
                return _context.Employees.Where(u => u.Id == employeeId).FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool verifyEmail(EmployeeEmailData employeeData)
        {
            try
            {

                if (!string.IsNullOrEmpty(employeeData.EmailId))
                {

                    if (employeeData.EmployeeId == 0)
                    {
                        var emailIdExist = _context.Employees.Where(w => w.Email.ToLower().Trim() == employeeData.EmailId.ToLower().Trim()).FirstOrDefault();
                        if (emailIdExist != null)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    } else
                    {
                        var emailIdExist = _context.Employees.Where(w => w.Email.ToLower() == employeeData.EmailId.ToLower() && w.Id != employeeData.EmployeeId).FirstOrDefault();
                        if (emailIdExist != null)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }

                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EmployeeSummary> getEmployees()
        {
            try
            {
                return _context.Employees.Select(u => new EmployeeSummary
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Mobile = u.Mobile,
                    Designation = u.Designation,
                    Gender = u.Gender,
                    ImageId = u.ImageId,
                    CreatedDate = u.CreatedDate,
                    Status = u.Status
                }).OrderByDescending(us => us.CreatedDate).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
