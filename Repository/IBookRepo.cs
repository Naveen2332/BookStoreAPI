using BookStore.Model;
using Microsoft.AspNetCore.JsonPatch;

namespace BookStore.Repository
{
    public interface IBookRepo
    {
        Task<List<BookModel>> GetAllBooksAsync();
        Task<BookModel> GetBookByIdAsync(int id);
        Task<string> AddBookAsync(BookModel bookModel);
        Task<string> UpdateBookAsync(BookModel bookModel);
        Task<string> UpdateBookPatchAsync(int id, JsonPatchDocument bookmodel);
        Task<bool> DeleteAsync(int id);
    }
}