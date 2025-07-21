using Books.Application.Models;
using Books.Contracts.Requests;
using Books.Contracts.Responses;

namespace Books.API.Mapping;

public static class ContractMapping
{
    public static Book MapToNewBook(this CreateBookRequest request)
    {
        return new Book
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Authors = request.Authors.ToList(),
            ISBN = request.ISBN,
            YearOfRelease = request.YearOfRelease,
            Genres = request.Genres.ToList(),
            IsLoan = false
        };
    }
    
    public static Book MapToBook(this UpdateBookRequest request , Guid id)
    {
        return new Book
        {
            Id = id,
            Title = request.Title,
            Authors = request.Authors.ToList(),
            ISBN = request.ISBN,
            YearOfRelease = request.YearOfRelease,
            Genres = request.Genres.ToList(),
            IsLoan = request.IsLoan
        };
    }

    public static BookResponse MapToBookResponse(this Book book)
    {
        return new BookResponse
        {
            Id = book.Id,
            Title = book.Title,
            Authors = book.Authors.ToList(),
            ISBN = book.ISBN,
            YearOfRelease = book.YearOfRelease,
            Genres = book.Genres,
            IsLoan = book.IsLoan
        };
    }
}