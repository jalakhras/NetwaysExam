using Netways.Application.Dtos;
using Netways.Application.Helpers;
using Netways.EntityFramworkCore.DBContext;
using Netways.EntityFramworkCore.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Netways.Application.Service
{
    public class UserService : IUserService
    {
        private readonly ApplicationDBContext _context;

        public UserService(ApplicationDBContext context)
        {
            _context = context;
        }

        public UserDto Authenticate(string loginName, string password)
        {

            if (!string.IsNullOrEmpty(loginName) && !string.IsNullOrEmpty(password))
            {
                var user = _context.Users.Include(x => x.Country).SingleOrDefault(x => x.LoginName == loginName);

                // check if LoginName exists
                if (user == null || !VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                    return null;

                // authentication successful
                var userDto = MappingToDto(user);
                return userDto;
            }
            return null;
        }

        public IEnumerable<UserDto> GetAll()
        {
            var usersDto = _context.Users.Include(x => x.Country).Select(user => new UserDto
            {
                Address = user.Address,
                CountryId = user.CountryId,
                DateofBirth = user.DateofBirth,
                DisplayName = user.DisplayName,
                IsActive = user.IsActive,
                LoginName = user.LoginName,
                ProfilePicture = user.ProfilePicture,
                Salary = user.Salary,
                Country = new CountryDto
                {
                    Id = user.Country != null? user.Country.Id:0,
                    Name = user.Country != null? user.Country.Name:string.Empty
                }


            }).ToList();
            return usersDto;

        } 
        public IEnumerable<CountryDto> GetCountries()
        {
            var countryDto = _context.Countries.Select(country => new CountryDto
            {
                    Id = country.Id,
                    Name = country.Name
            }).Where(x=>!string.IsNullOrEmpty(x.Name)).ToList();
            return countryDto;

        }

        public UserDto GetById(string loginName)
        {
            var user = _context.Users.Include(x => x.Country).FirstOrDefault(x => x.LoginName == loginName);
            UserDto userDto = MappingToDto(user);
            return userDto;

        }


        public async Task<UserDto> CreateAsync(UserDto userDto)
        {
            // validation
            if (string.IsNullOrWhiteSpace(userDto.Password))
                throw new AppException("Password is required");

            if (_context.Users.Any(x => x.LoginName == userDto.LoginName))
                throw new AppException("LoginName \"" + userDto.LoginName + "\" is already taken");
            // map dto to entity
            User user = MappingToEntity(userDto);
            byte[] passwordSalt;
            CreatePasswordHash(userDto.Password, out byte[] passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);
            _context.SaveChanges();
            return userDto;
        }
        private static UserDto MappingToDto(User user) => new UserDto
        {
            Address = user.Address,
            CountryId = user.CountryId,
            DateofBirth = user.DateofBirth,
            DisplayName = user.DisplayName,
            IsActive = user.IsActive,
            LoginName = user.LoginName,
            ProfilePicture = user.ProfilePicture,
            Salary = user.Salary,
            Country = new CountryDto
            {
                Id = user.Country != null ? user.Country.Id : 0,
                Name = user.Country != null ? user.Country.Name : string.Empty
            }
        };

        private static User MappingToEntity(UserDto userDto) => new User
        {
            Address = userDto.Address,
            CountryId = userDto.CountryId,
            DateofBirth = userDto.DateofBirth,
            DisplayName = userDto.DisplayName,
            IsActive = userDto.IsActive,
            LoginName = userDto.LoginName,
            ProfilePicture = userDto.ProfilePicture,
            Salary = userDto.Salary,
            Country = new Country
            {
                Id = userDto.Country != null ? userDto.Country.Id : 0,
                Name = userDto.Country != null ? userDto.Country.Name : string.Empty
            }
        };

        public void Update(UserDto userParam)
        {
            var user = _context.Users.Find(userParam.LoginName);

            if (user == null)
                throw new AppException("User not found");

            if (userParam.LoginName != user.LoginName)
            {
                // LoginName has changed so check if the new LoginName is already taken
                if (_context.Users.Any(x => x.LoginName == userParam.LoginName))
                    throw new AppException("LoginName " + userParam.LoginName + " is already taken");
            }

            // update user properties
            user.Address = userParam.Address;
            user.DisplayName = userParam.DisplayName;
            user.CountryId = userParam.CountryId;
            user.LoginName = userParam.LoginName;
            user.DateofBirth = userParam.DateofBirth;
            user.IsActive = userParam.IsActive;
            user.Salary = userParam.Salary;

            // update password if it was entered
            if (!string.IsNullOrWhiteSpace(userParam.Password))
            {
                CreatePasswordHash(userParam.Password, out byte[] passwordHash, out byte[] passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            _context.Users.Update(user);
            _context.SaveChanges();
        }
        public void UpdateImagePath(string LoginName , string imagePath)
        {
            var user = _context.Users.Find(LoginName);
            if (user == null)
                throw new AppException("User not found");
            user.ProfilePicture =imagePath;
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(string loginName)
        {
            if (loginName is null)
            {
                throw new ArgumentNullException(nameof(loginName));
            }

            var user = _context.Users.Find(loginName);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        // private helper methods
        //private IQueryable<User> GetAllIncluding(params Expression<Func<User, object>>[] includeProperties)
        //{
        //    IQueryable<User> queryable = _context.Users.AsNoTracking();

        //    if (includeProperties != null)
        //        queryable = includeProperties.Aggregate(queryable, (current, includeProperty) => current.Include(includeProperty));
        //    return queryable;
        //}
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
