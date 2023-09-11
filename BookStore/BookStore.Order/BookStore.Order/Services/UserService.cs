using BookStore.Order.Entity;
using BookStore.Order.Interface;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace BookStore.Order.Services
{
    /// <summary>
    /// Order Service
    /// </summary>
    public class UserService : IUserService
    {
        private readonly HttpClient httpClient;
        public UserService(IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient("UserApi");
        }
        /// <summary>
        /// Gettting user info from User project using HttpClient And IhttpClient Faactory (MicroService)
        /// </summary>
        /// <param name="token">Jwt token from header</param>
        /// <returns>Book Info</returns>
        public async Task<UserEntity> GetUserProfile(string token)
        {
            try
            {
                UserEntity userEntity = null;
                //string url = "https://localhost:7065/api/User/userInfo";
                //HttpClient client = new HttpClient();
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);//Defult Request Header Modify Every  subSequent Request of httpClient Request.
                //HttpResponseMessage httpResponse = await client.GetAsync(url);//Get All Information About http Response.
                //if (httpResponse.IsSuccessStatusCode)
                //{
                //    string content = await httpResponse.Content.ReadAsStringAsync();//Store the response body as String in content.
                //    ResponseEntity responseEntity = JsonConvert.DeserializeObject<ResponseEntity>(content);//convert string body to ResponseEntity Object.
                //    if (responseEntity.IsSucess)
                //    {
                //        userEntity = JsonConvert.DeserializeObject<UserEntity>(responseEntity.Data.ToString());//Convert Response object data to UserEntity Object.
                //    }
                //}
                //return userEntity;


                //Using Http Factory
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);//Defult Request Header Modify Every  subSequent Request of httpClient Request.
                HttpResponseMessage responseMessage = await httpClient.GetAsync("userInfo");
                if (responseMessage.IsSuccessStatusCode)
                {
                    string content = await responseMessage.Content.ReadAsStringAsync();
                    ResponseEntity responseEntity = JsonConvert.DeserializeObject<ResponseEntity>(content);
                    if (responseEntity.IsSucess)
                    {
                        userEntity = JsonConvert.DeserializeObject<UserEntity>(responseEntity.Data.ToString());
                    }
                }
                return userEntity;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
