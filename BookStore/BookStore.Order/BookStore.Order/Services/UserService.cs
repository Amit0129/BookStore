using BookStore.Order.Entity;
using BookStore.Order.Interface;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace BookStore.Order.Services
{
    public class UserService : IUserService
    {
        public async Task<UserEntity> GetUserProfile(string token)
        {
            UserEntity userEntity = null;
            string url = "https://localhost:7065/api/User/userInfo";
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage httpResponse = await client.GetAsync(url);
            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();
                ResponseEntity responseEntity = JsonConvert.DeserializeObject<ResponseEntity>(content);
                if (responseEntity.IsSucess)
                {
                    userEntity = JsonConvert.DeserializeObject<UserEntity>(responseEntity.Data.ToString());
                }
            }
            return userEntity;
        }
    }
}
