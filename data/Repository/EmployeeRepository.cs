using data.Context;
using data.Interface;
using data.Models;
using data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using data.Helper;

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
                employee.CreatedDate = DateTime.Now;
                employee.UpdatedDate = DateTime.Now;
                //employee.Status = "Active";
                employee.ImageId = employee.ImageId;
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
                    employee.UpdatedDate = DateTime.Now;
                    _context.Employees.Update(employeeData);
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
                 List<Employee> employees =  _context.Employees.ToList();


                List<EmployeeSummary> employeeSummary = employees.Select(u => new EmployeeSummary
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
                }).OrderBy(us => us.Id).ToList();

                for (var count = 0; count < employees.Count; count++)
                {
                    List<string> ar = new List<string>();
                    if (employees[count].Bca)
                    {
                        ar.Add("BCA");
                    }
                    if (employees[count].Mca)
                    {
                        ar.Add("MCA");
                    }
                    if (employees[count].Bsc)
                    {
                        ar.Add("BSC");
                    }

                    employeeSummary[count].Course = string.Join(",", ar); 
                }
                return employeeSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string SaveFileAttachment(FileAttachement fileAttachement)
        {
            try
            {
                _context.FileAttachements.Add(fileAttachement);
                _context.SaveChanges();
                return fileAttachement.FileID.ToString()+fileAttachement.Extension.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
