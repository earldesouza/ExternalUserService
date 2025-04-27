using ExternalUserService.Clients;
using ExternalUserService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalUserService.Services
{
    public class ExternalUserServiceImpl : IExternalUserService
    {
        private readonly IReqResApiClient _apiClient;

        public ExternalUserServiceImpl(IReqResApiClient apiClient)
        {
            _apiClient = apiClient;
        }


        public async Task<User> GetUserByIdAsync(int userId)
        {
            var dto = await _apiClient.GetUserByIdAsync(userId);
            return MapToUser(dto);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var users = new List<User>();
            int page = 1;

            while (true)
            {
                var dtos = await _apiClient.GetUsersByPageAsync(page);
                if (!dtos.Any()) break;
                users.AddRange(dtos.Select(MapToUser));
                page++;
            }

            return users;
        }

        private User MapToUser(UserDto dto)
        {
            return new User
            {
                Id = dto.Id,
                Email = dto.Email,
                FirstName = dto.First_Name,
                LastName = dto.Last_Name
            };
        }
    }
}
