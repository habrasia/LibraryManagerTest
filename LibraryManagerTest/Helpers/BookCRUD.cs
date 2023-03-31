using LibraryManagerTest.Models;
using System.Configuration;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace LibraryManagerTest.Helpers
{
    internal class BookCRUD
    {
        private HttpClient _client;

        public BookCRUD()
        {
            _client = new HttpClient();

            var url = ConfigurationManager.AppSettings["apiUrl"];

            _client.BaseAddress = new Uri(url);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        internal async Task<Book> AddBook(Book book)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync($"books", book);

            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK, $"Status response failed for add {book.ToString()}");

            return await response.Content.ReadAsAsync<Book>();
        }

        internal async Task<List<Book>> GetBooksByTitle(string? title = null)
        {
            var response = await _client.GetAsync($"books?title={title}");

            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK, $"Status response failed for retriving book/books by \"{title}\" title");

            return await response.Content.ReadAsAsync<List<Book>>();
        }

        internal async Task<Book> GetBookById(int? id = null)
        {
            var response = await _client.GetAsync($"books/{id}");

            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK, $"Status response failed when retriving book by its id: \"{id}\"");

            return await response.Content.ReadAsAsync<Book>();
        }

        internal async Task<Book> UpdateBook(int id, Book book)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync($"books/{id}", book);

            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK, $"Status response failed to update book of id {id} to {book.ToString()}");

            return await response.Content.ReadAsAsync<Book>();
        }

        internal async Task DeleteBook(int id)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"books/{id}");

            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent, $"Status response failed when deleting book by its id: \"{id}\"");
        }
    }
}
