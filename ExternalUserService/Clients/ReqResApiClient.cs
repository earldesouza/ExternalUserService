using ExternalUserService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ExternalUserService.Clients
{
    public class ReqResApiClient : IReqResApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };

        public ReqResApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<UserDto> GetUserByIdAsync(int userId)
        {
            var response = await _httpClient.GetAsync($"users/{userId}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var wrapper = JsonSerializer.Deserialize<ApiSingleUserResponse>(json, _jsonOptions);
            return wrapper.Data;
        }

        public async Task<IEnumerable<UserDto>> GetUsersByPageAsync(int page)
        {
            var response = await _httpClient.GetAsync($"users?page={page}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var wrapper = JsonSerializer.Deserialize<ApiUserListResponse>(json, _jsonOptions);
            return wrapper.Data;
        }
    }

    public class ApiSingleUserResponse
    {
        public UserDto Data { get; set; }
    }

    public class ApiUserListResponse
    {
        public List<UserDto> Data { get; set; }
    }
}
