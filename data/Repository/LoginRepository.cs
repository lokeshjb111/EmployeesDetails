using data.Context;
using data.Interface;
using data.ViewModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using data.Models;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.IO;

namespace data.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        public LoginRepository(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public LoginViewModel checkUserCredential(LoginDetails loginDetails)
        {
            try
            {
                LoginViewModel loginViewModel = new LoginViewModel();
                User user = _context.Users.Where(u => u.Email.Trim().ToLower() == loginDetails.Email.Trim().ToLower()).FirstOrDefault();

                if (user != null)
                {
                    if (loginDetails.Password.Trim() == Decrypt(user.Password.Trim()))
                    {
                        loginViewModel.Name = user.Name;
                        loginViewModel.IsValid =  true;
                    }
                    else
                    {
                        loginViewModel.IsValid = false;
                    }
                }
                else
                {
                    loginViewModel.IsValid = false;
                }
                return loginViewModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int getTokenExpireTime()
        {
            try
            {
                return Convert.ToInt32(_configuration["TokenExpireTime"]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string getToken(string userId)
        {
            try
            {
                SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim> {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, userId)
                };

                JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Issuer"],
                        claims,
                        expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["TokenExpireTime"])),
                        signingCredentials: signingCredentials
                 );

                return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string Decrypt(string hashText)
        {
            try
            {
                string key = "HBGFSESRTNGJ477845435FHBF35DFGJY";
                byte[] hashBytes = Convert.FromBase64String(hashText);
                using (Aes aes = Aes.Create())
                {
                    Rfc2898DeriveBytes pbkd = new Rfc2898DeriveBytes(key, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    aes.Key = pbkd.GetBytes(32);
                    aes.IV = pbkd.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(hashBytes, 0, hashBytes.Length);
                            cs.Close();
                        }

                        return Encoding.Unicode.GetString(ms.ToArray());
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string Encrypt(string text)
        {
            try
            {
                string key = "HBGFSESRTNGJ477845435FHBF35DFGJY";
                byte[] textBytes = Encoding.Unicode.GetBytes(text);
                using (Aes aes = Aes.Create())
                {
                    Rfc2898DeriveBytes pbkd = new Rfc2898DeriveBytes(key, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    aes.Key = pbkd.GetBytes(32);
                    aes.IV = pbkd.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(textBytes, 0, textBytes.Length);
                            cs.Close();
                        }
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
