using Books.Application.Models;

namespace Books.Application.Services;

public interface IBookService
{
    Task<bool> CreateBookAsync(Book book);
    
    Task<Book?> GetByIdAsync(Guid id);
    
    Task<IEnumerable<Book>> GetAllAsync();
    
    Task<Book?> UpdateBookAsync(Book book);
    
    Task<bool> DeleteBookAsync(Guid id);
    
    Task <Book?> GetByIsbnAsync(string isbn);
    
}