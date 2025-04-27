using ExternalUserService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalUserService.Clients
{
    public interface IReqResApiClient
    {
        Task<UserDto> GetUserByIdAsync(int userId);
        Task<IEnumerable<UserDto>> GetUsersByPageAsync(int page);

        Task<IEnumerable<UserDto>> GetAllUsersAsync();
    }
}
