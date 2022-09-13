using obg.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.Domain.Entities
{
    public class Administrator
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public RoleUser Role { get; set; }
        public string RegisterDate { get; set; }

        public Administrator(){ }

    }
}
