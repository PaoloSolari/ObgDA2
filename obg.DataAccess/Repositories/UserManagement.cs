using Microsoft.EntityFrameworkCore;
using obg.DataAccess.Context;
using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace obg.DataAccess.Repositories
{
    public class UserManagement : IUserManagement
    {
        private ObgContext ObgContext { get; set; }
        public UserManagement(ObgContext obgContext)
        {
            this.ObgContext = obgContext;
        }

        public void InsertUser(User user)
        {
            ObgContext.Users.Add(user);
            ObgContext.SaveChanges();
        }

        public IEnumerable<User> GetUsers()
        {
            return ObgContext.Users.ToList();
        }

        public User GetUserByName(string name)
        {
            return ObgContext.Users.Where<User>(u => u.Name == name).AsNoTracking().FirstOrDefault();
        }

        public void UpdateUser(User user)
        {
            ObgContext.Users.Attach(user);
            ObgContext.Entry(user).State = EntityState.Modified;
            ObgContext.SaveChanges();
        }

    }
}
