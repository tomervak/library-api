using System.ComponentModel.DataAnnotations;

namespace Books.Contracts.Requests;

public class CreateBookRequest
{
    public required  string Title { get; init; }
    public required IEnumerable<string> Authors { get; init; }
    public required string ISBN { get; init; }
    public required int YearOfRelease { get; init; }
    public required  IEnumerable<string> Genres { get; init; } = [];
    
}