using Netways.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Netways.Application.Service
{
    public interface IUserService
    {
        UserDto Authenticate(string loginName, string password);
        IEnumerable<UserDto> GetAll();
        IEnumerable<CountryDto> GetCountries();
        UserDto GetById(string loginName);
        Task<UserDto> CreateAsync(UserDto userDto);
        void Update(UserDto user);
        void Delete(string loginName);
        void UpdateImagePath(string LoginName, string imagePath);
    }
}
