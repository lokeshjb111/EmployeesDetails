using data.Context;
using data.Helper;
using data.Interface;
using data.Models;
using data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CS_Tech.Controllers
{
    public class EmployeeController : Controller
    {
        private IEmployeeRepository _repository;
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _env;
        private readonly DataContext _context;
        public EmployeeController(IEmployeeRepository repository, IConfiguration config, IWebHostEnvironment env, DataContext context)
        {
            _repository = repository;
            _config = config;
            _env = env;
            _context = context;
        }

        [Authorize]
        [HttpGet, Route("employeeCon/getEmployees")]
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

        [Authorize]
        [HttpPost, Route("employeeCon/addEmployee")]
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

        [Authorize]
        [HttpPost, Route("employeeCon/updateEmployee")]
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

        [Authorize]
        [HttpGet, Route("employeeCon/deleteEmployee/{employeeId}")]
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

        [Authorize]
        [HttpGet, Route("employeeCon/getEmployee/{employeeId}")]
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

        [HttpPost, Route("employeeCon/verifyEmail")]
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

        [Authorize]
        [RequestSizeLimit(5000000)]
        [HttpPost, Route("employeeCon/UploadDoc/{key}")]
        public string UploadFile(string key)
        {
            string keyUploaded = string.Empty;
            try
            {

                //var path = _env.ContentRootPath + "\\wwwroot\\Employees";
                var path = Directory.GetCurrentDirectory() + "/Images/Employees";


                FileAttachement fileAttachement = new FileAttachement();
                var request = Request.Form.Files[0];
                string[] extension = request.FileName.Split(".");
                fileAttachement.Extension = "." + extension[extension.Length - 1];

                if (extension[extension.Length - 1].ToString().ToLower() == "png" || extension[extension.Length - 1].ToString().ToLower() == "jpeg"
                    || extension[extension.Length - 1].ToString().ToLower() == "jpg")
                {
                    fileAttachement.FileID = Guid.NewGuid();

                    var status = SaveFileStream(path, path + "\\" + fileAttachement.FileID + "." + extension[extension.Length - 1], request.OpenReadStream());



                    fileAttachement.RefID = Guid.Parse(key);
                    fileAttachement.FileName = request.FileName;
                    fileAttachement.ContentType = request.ContentType;
                    keyUploaded = _repository.SaveFileAttachment(fileAttachement);
                }
                else
                {

                    return "Invalid File Format";
                }


                

            }
            catch (Exception ex)
            {
                Utility.LogException(ex, _env.ContentRootPath);
            }
            return keyUploaded;
        }

        private bool SaveFileStream(string rootFolderPath, string fileFullpath, Stream stream)
        {
            try
            {
                if (!System.IO.Directory.Exists(rootFolderPath))
                {
                    System.IO.Directory.CreateDirectory(rootFolderPath);
                }
                var fileStream = new FileStream(fileFullpath, FileMode.Create, FileAccess.Write);
                stream.CopyTo(fileStream);
                fileStream.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
