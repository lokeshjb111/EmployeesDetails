using data.Interface;
using data.Models;
using data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CS_Tech.Controllers
{
    public class EmployeeController : Controller
    {
        private IEmployeeRepository _repository;
        public EmployeeController(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        [HttpGet, Route("employee/getEmployees")]
        public IActionResult getEmployees()
        {
            try
            {
                List<EmployeeSummary> employees = _repository.getEmployees();
                return Ok(new
                {
                    Message = "",
                    HttpStatus = 200,
                    Result = employees
                });
            }
            catch (Exception)
            {
                return Ok(new
                {
                    Message = "Error While Fetching Data",
                    HttpStatus = 500,
                    Result = ""
                });
            }
        }

        [HttpPost, Route("employee/addEmployee")]
        public IActionResult addEmployee([FromBody] Employee employee)
        {
            try
            {
                string status = _repository.addEmployee(employee);
                return Ok(new
                {
                    Message = status,
                    HttpStatus = 200,
                    Result = status
                });
            }
            catch (Exception)
            {
                return Ok(new
                {
                    Message = "Error While Saving Employee",
                    HttpStatus = 500,
                    Result = ""
                });
            }
        }

        [HttpPost, Route("employee/updateEmployee")]
        public IActionResult updateEmployee([FromBody] Employee employee)
        {
            try
            {
                string status = _repository.updateEmployee(employee);
                return Ok(new
                {
                    Message = status,
                    HttpStatus = 200,
                    Result = status
                });
            }
            catch (Exception)
            {
                return Ok(new
                {
                    Message = "Error While Updating Data",
                    HttpStatus = 500,
                    Result = ""
                });
            }
        }

        [HttpGet, Route("employee/deleteEmployee")]
        public IActionResult deleteEmployee(int employeeId)
        {
            try
            {
                string status = _repository.deleteEmployee(employeeId);
                return Ok(new
                {
                    Message = "",
                    HttpStatus = 200,
                    Result = status
                });
            }
            catch (Exception)
            {
                return Ok(new
                {
                    Message = "Error While Deleting Data",
                    HttpStatus = 500,
                    Result = ""
                });
            }
        }

        [HttpGet, Route("employee/getEmployee")]
        public IActionResult getEmployee(int employeeId)
        {
            try
            {
                Employee employee = _repository.getEmployee(employeeId);
                if (employee != null)
                {
                    return Ok(new
                    {
                        Message = "",
                        HttpStatus = 200,
                        Result = employee
                    });
                }
                else
                {
                    return Ok(new
                    {
                        Message = "Invalid Employee Id",
                        HttpStatus = 500,
                        Result = ""
                    });
                }
                
            }
            catch (Exception)
            {
                return Ok(new
                {
                    Message = "Error While Fetching Data",
                    HttpStatus = 500,
                    Result = ""
                });
            }
        }

        [HttpPost, Route("employee/verifyEmail")]
        public IActionResult verifyEmail([FromBody] EmployeeEmailData employeeData)
        {
            try
            {
                bool status = _repository.verifyEmail(employeeData);
                return Ok(new
                {
                    Message = "",
                    HttpStatus = 200,
                    Result = status
                });
            }
            catch (Exception)
            {
                return Ok(new
                {
                    Message = "Error While Verifying Email",
                    HttpStatus = 500,
                    Result = ""
                });
            }
        }
    }
}
