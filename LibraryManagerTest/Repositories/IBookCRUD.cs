﻿using LibraryManagerTest.Models;

namespace LibraryManagerTest.Repositories
{
    public interface IBookCRUD
    {
        Task<HttpResponseMessage> AddBookAsync(Book book);
        Task<HttpResponseMessage> GetBooksByTitleAsync(string? title);
        Task<HttpResponseMessage> GetBookByIdAsync(int? id);
        Task<HttpResponseMessage> UpdateBookAsync(int id, Book book);
        Task<HttpResponseMessage> DeleteBookAsync(int id);
    }
}
