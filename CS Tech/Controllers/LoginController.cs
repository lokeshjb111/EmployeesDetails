using data.Helper;
using data.Interface;
using data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static data.Helper.Common;

namespace CS_Tech.Controllers
{
    public class LoginController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILoginRepository _repository;
        public LoginController(IWebHostEnvironment env, ILoginRepository repository)
        {
            _env = env;
            _repository = repository;
        }

        [AllowAnonymous]
        [HttpPost, Route("login/authenticateUser")]
        public IActionResult VerifyUser([FromBody] LoginDetails loginDetails)
        {
            try
            {

                LoginViewModel loginViewDetails = _repository.checkUserCredential(loginDetails);

                if (loginViewDetails.IsValid)
                {
                    var tokenExpireTime = _repository.getTokenExpireTime();
                    var bearertoken = _repository.getToken(loginDetails.Email);
                    return Ok(new Response
                    {
                        Result = new
                        {
                            isvalid = true,
                            token = bearertoken,
                            expire = DateTime.Now.AddMinutes(tokenExpireTime),
                            Name = loginViewDetails.Name
                        },
                        Message = "",
                        HttpStatus = 200
                    });
                }
                else
                {
                    return Ok(new Response
                    {
                        Result = new
                        {
                            token = "",
                            isvalid = false
                        },
                        Message = "Invalid User",
                        HttpStatus = 200

                    });
                }

            }
            catch (Exception ex)
            {
                Utility.LogException(ex, _env.ContentRootPath);
                return Ok(new Response
                {
                    HttpStatus = 500,
                    Message = "Error While Authenticating User",
                    Result = ""
                });
            }



        }
    }
}
