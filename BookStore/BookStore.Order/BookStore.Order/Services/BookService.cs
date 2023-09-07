using BookStore.Order.Entity;
using BookStore.Order.Interface;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Net.WebRequestMethods;

namespace BookStore.Order.Services
{
    public class BookService : IBookService
    {
        private readonly HttpClient _httpMessingClient;
        public BookService(IHttpClientFactory httpClientFactory)
        {
            _httpMessingClient = httpClientFactory.CreateClient("BookApi");//A method CreateClient which return the http client Object
        }
        public async Task<BookEntity> GetBookById(long bookId)
        {
            BookEntity bookEntity = null;
            //string url = $"https://localhost:7256/api/Books/GetById/{bookId}";
            //HttpClient client = new HttpClient();
            //HttpResponseMessage responseMessage = await client.GetAsync(url);
            //if (responseMessage.IsSuccessStatusCode)
            //{
            //    string content = await responseMessage.Content.ReadAsStringAsync();
            //    ResponseEntity response = JsonConvert.DeserializeObject<ResponseEntity>(content);
            //    if (response.IsSucess)
            //    {
            //        bookEntity = JsonConvert.DeserializeObject<BookEntity>(response.Data.ToString());
            //    }
            //}
            //return bookEntity;


            //IHttpFactory
            HttpResponseMessage responseMessage = await _httpMessingClient.GetAsync($"GetById/{bookId}");
            if (responseMessage.IsSuccessStatusCode)
            {
                string content = await responseMessage.Content.ReadAsStringAsync();
                ResponseEntity response = JsonConvert.DeserializeObject<ResponseEntity>(content);
                if (response.IsSucess)
                {
                    bookEntity = JsonConvert.DeserializeObject<BookEntity>(response.Data.ToString());
                }
            }
            return bookEntity;
        }
    }
}
