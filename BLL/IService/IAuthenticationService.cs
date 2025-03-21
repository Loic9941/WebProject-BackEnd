﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.IService
{
    public interface IAuthenticationService
    {
        public void RegisterUser(string username, string password);
        public string Login(string username, string password);
    }
}
