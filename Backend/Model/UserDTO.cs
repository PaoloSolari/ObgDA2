using obg.Domain.Entities;
using obg.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class UserDTO
    {
        public string Name { get; private set; }
        public int Code { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string Address { get; private set; }
        //public RoleUser Role { get; set; }
        //public string RegisterDate { get; set; }

        public UserDTO(User user)
        {
            this.Name = user.Name;
            this.Code = user.Code;
            this.Email = user.Email;
            this.Password = user.Password;
            this.Address = user.Address;
        }
    }
}
