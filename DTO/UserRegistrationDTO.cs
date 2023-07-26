using System;
namespace UserApi.DTO
{
	public class UserRegistrationDTO
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}

