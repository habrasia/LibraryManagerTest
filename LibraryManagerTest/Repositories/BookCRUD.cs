using LibraryManagerTest.Models;
using LibraryManagerTest.Repositories;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace LibraryManagerTest.Helpers
{
    public class BookCRUD : IBookCRUD
    {
        private HttpClient _client;
        private string _url = "http://localhost:9000/api/";

        public BookCRUD()
        {
            _client = new HttpClient();

            _client.BaseAddress = new Uri(_url);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<HttpResponseMessage> AddBookAsync(Book book)
        {
            var response = await _client.PostAsJsonAsync($"books", book);
            return response;
        }

        public async Task<HttpResponseMessage> GetBooksByTitleAsync(string? title = null)
        {
            var response = await _client.GetAsync($"books?title={title}");

            //Assert.AreEqual(response.StatusCode, HttpStatusCode.OK, $"Status response failed for retriving book/books by \"{title}\" title");

            //return await response.Content.ReadAsAsync<List<Book>>();
            return response;
        }

        public async Task<HttpResponseMessage> GetBookByIdAsync(int? id = null)
        {
            var response = await _client.GetAsync($"books/{id}");

            //Assert.AreEqual(response.StatusCode, HttpStatusCode.OK, $"Status response failed when retriving book by its id: \"{id}\"");

            //return await response.Content.ReadAsAsync<Book>();

            return response;
        }

        public async Task<Book> UpdateBookAsync(int id, Book book)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync($"books/{id}", book);

            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK, $"Status response failed to update book of id {id} to {book.ToString()}");

            return await response.Content.ReadAsAsync<Book>();
        }

        public async Task<HttpResponseMessage> DeleteBookAsync(int id)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"books/{id}");

            //Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent, $"Status response failed when deleting book by its id: \"{id}\"");
            return response;
        }
    }
}
