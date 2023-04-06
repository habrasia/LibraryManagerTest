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
            return response;
        }

        public async Task<HttpResponseMessage> GetBookByIdAsync(int? id = null)
        {
            var response = await _client.GetAsync($"books/{id}");
            return response;
        }

        public async Task<HttpResponseMessage> UpdateBookAsync(int id, Book book)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync($"books/{id}", book);
            return response;
        }

        public async Task<HttpResponseMessage> DeleteBookAsync(int id)
        {
            var response = await _client.DeleteAsync($"books/{id}");
            return response;
        }
    }
}
