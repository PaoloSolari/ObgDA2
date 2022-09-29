using obg.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace obg.Domain.Entities
{
    public class User
    {
        //[Key] public int Id { get; set; }
        [Key] public string Name { get; set; }
        public int Code { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public RoleUser Role { get; set; }
        public string RegisterDate { get; set; }

        public User() { }
        public User(string name, int code, string email, string password, string address, RoleUser role, string registerDate)
        {
            Name = name;
            Code = code;
            Email = email;
            Password = password;
            Address = address;
            Role = role;
            RegisterDate = registerDate;
        }
    }

}
