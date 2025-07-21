using Books.Application.Models;

namespace Books.Application.Repositories;

public class BookRepository : IBookRepository
{
    private readonly List<Book> _books = new();
    public Task<bool> CreateBookAsync(Book book)
    {
        _books.Add(book);
        return Task.FromResult(true);
    }

    public Task<Book?> GetByIdAsync(Guid id)
    {
        return Task.FromResult(_books.Find((book => book.Id == id )));
    }

    public Task<IEnumerable<Book>> GetAllAsync()
    {
        return Task.FromResult(_books.AsEnumerable());
    }

    public Task<bool> UpdateBookAsync(Book book)
    {
        var index = _books.FindIndex(book => book.Id == book.Id);
        if (index > 0)
            return Task.FromResult(false);
        _books[index] = book;
        return Task.FromResult(true);
    }

    public Task<bool> DeleteBookAsync(Guid id)
    {
        int count = _books.RemoveAll((book => book.Id == id));
        bool result = count > 0;
        return Task.FromResult(result);
    }
}