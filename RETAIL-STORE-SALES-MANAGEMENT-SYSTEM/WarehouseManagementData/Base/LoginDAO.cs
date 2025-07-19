using Microsoft.EntityFrameworkCore;
using WarehouseManagementData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagementData.Base
{
    public class LoginDAO
    {
        private readonly StoreManagementDbContext _context;

        private static LoginDAO instance = null!;

        private static readonly object instanceLock = new object();

        public LoginDAO()
        {
            _context = new StoreManagementDbContext();
        }

        public static LoginDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new LoginDAO();
                    }
                    return instance;
                }
            }
        }
        public User checkLogin(string userName, string password)
        {
            try
            {
                var check = _context.Users.Where(u => u.Username!.Equals(userName) && u.Password!.Equals(password)).FirstOrDefault();

                if (check != null)
                {
                    return check;
                }
                throw new Exception();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
