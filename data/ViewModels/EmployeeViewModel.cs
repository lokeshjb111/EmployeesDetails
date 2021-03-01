using System;
using System.Collections.Generic;
using System.Text;

namespace data.ViewModels
{
    public class EmployeeSummary
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Designation { get; set; }
        public string Gender { get; set; }
        public string Course { get; set; }
        public string ImageId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }
    }

    public class LoginDetails
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginViewModel
    {
        public string Name { get; set; }
        public bool IsValid { get; set; }
    }
    public class EmployeeEmailData
    {
        public string EmailId { get; set; }
        public int EmployeeId { get; set; }
    }

}
