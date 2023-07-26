using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserApi.Entities
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public DateTime DateOfBirth { get; set; }
    }

    public class UserLoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}