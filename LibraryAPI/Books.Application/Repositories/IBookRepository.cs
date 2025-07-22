using Books.Application.Models;

namespace Books.Application.Repositories;

public interface IBookRepository
{
    Task<bool> CreateBookAsync(Book book);
    
    Task<Book?> GetByIdAsync(Guid id);
    
    Task<IEnumerable<Book>> GetAllAsync();
    
    Task<bool> UpdateBookAsync(Book book);
    
    Task<bool> DeleteBookAsync(Guid id);
    
    Task<bool> ExistByIdAsync(Guid id);
    
    Task<Book?> GetByIsbnAsync(string isbn);
    
}