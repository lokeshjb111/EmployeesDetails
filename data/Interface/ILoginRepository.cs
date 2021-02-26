using data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace data.Interface
{
    public interface ILoginRepository
    {
        public LoginViewModel checkUserCredential(LoginDetails loginDetails);
        public int getTokenExpireTime();
        public string getToken(string email);
    }
}
