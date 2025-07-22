using Books.Application.Models;
using Books.Application.Repositories;
using Books.Application.Validators;
using FluentValidation;

namespace Books.Application.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly BookValidator _bookValidator;

    public BookService(IBookRepository bookRepository, BookValidator bookValidator)
    {
        _bookRepository = bookRepository;
        _bookValidator = bookValidator;
    }

    public async Task<bool> CreateBookAsync(Book book)
    {
        await _bookValidator.ValidateAndThrowAsync(book);
        return await _bookRepository.CreateBookAsync(book);
    }

    public async Task<Book?> GetByIdAsync(Guid id)
    {
        return await _bookRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        return await _bookRepository.GetAllAsync();
    }

    public async Task<Book?> UpdateBookAsync(Book book)
    {
        await _bookValidator.ValidateAndThrowAsync(book);
        var bookExists = await _bookRepository.ExistByIdAsync(book.Id);
        if (!bookExists)
            return null;
        await _bookRepository.UpdateBookAsync(book);
        return book;

    }

    public async Task<bool> DeleteBookAsync(Guid id)
    {
        return await _bookRepository.DeleteBookAsync(id);
    }

    public async Task<Book?> GetByIsbnAsync(string isbn)
    {
        return await _bookRepository.GetByIsbnAsync(isbn);
    }
}