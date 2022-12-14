using CloudCustomer.Services.Config;
using CloudCustomer.Services.Models;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace CloudCustomer.Services
{

    public interface IUsersService
    {
        public Task<List<User>> GetAllUsers();
    }
    public class UsersService : IUsersService
    {
        private readonly HttpClient _httpClient;
        private readonly UsersAPIOptions _apiConfig;

        public UsersService(
            HttpClient httpClient, 
            IOptions<UsersAPIOptions> apiConfig)
        {
            _httpClient = httpClient;
            _apiConfig = apiConfig.Value;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var userResponse = await _httpClient.GetAsync(_apiConfig.Endpoint);
            if (userResponse.StatusCode == System.Net.HttpStatusCode.NotFound) {
                return new List<User>();
            }
            var responseContent = userResponse.Content;
            var allUsers = await responseContent.ReadFromJsonAsync<List<User>>();
            return allUsers.ToList();
        }
    }
}