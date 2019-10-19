using System;

namespace Netways.Application.Dtos
{
    public class UserDto
    {
        public string LoginName { get; set; }
        public string DisplayName { get; set; }
        public DateTime DateofBirth { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public int Salary { get; set; }
        public string ProfilePicture { get; set; }
        public string Password { get; set; }
        public int CountryId { get; set; }
        public virtual CountryDto Country { get; set; }
        public string CountryName { get=>Country?.Name;  }
    } 
    public class UserLoginDto
    {
        public string LoginName { get; set; }
        public string Password { get; set; }
    
    }
}
