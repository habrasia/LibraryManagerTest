using LibraryManagerTest.Models;

namespace LibraryManagerTest.Repositories
{
    public interface IBookCRUD
    {
        Task<HttpResponseMessage> AddBookAsync(Book book);
        Task<IEnumerable<Book>> GetBooksByTitleAsync(string? title);
        Task<Book> GetBookByIdAsync(int? id);
        Task<Book> UpdateBookAsync(int id, Book book);
        Task DeleteBookAsync(int id);
    }
}
