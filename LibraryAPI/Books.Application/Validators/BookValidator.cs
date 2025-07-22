using Books.Application.Models;
using Books.Application.Repositories;
using Books.Application.Services;
using FluentValidation;

namespace Books.Application.Validators;

public class BookValidator : AbstractValidator<Book>
{
    private readonly IBookRepository  _bookRepository;
    
    public BookValidator(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
        
        RuleFor(x => x.Id)
            .NotEmpty();
        
        RuleFor(x => x.Title)
            .NotEmpty();
        
        RuleFor(x => x.Authors)
            .NotEmpty();
        
        RuleFor(x => x.ISBN)
            .MustAsync(ValidateIsbn)
            .WithMessage("Book already exists.");
        
        RuleFor(x => x.YearOfRelease)
            .LessThanOrEqualTo(DateTime.UtcNow.Year);
        
        RuleFor(x => x.Genres)
            .NotEmpty();
    }

    private async Task<bool> ValidateIsbn( Book book, string isbn, CancellationToken token = default)
    {
        var existingMovie = await _bookRepository.GetByIsbnAsync(isbn);

        if (existingMovie is not null)
        {
            return book.Id == existingMovie.Id;
        }

        return existingMovie is null;
    }
}