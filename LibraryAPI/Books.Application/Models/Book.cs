namespace Books.Application.Models;

public class Book
{
    public required Guid Id { get; init; }
    public required  string Title { get; set; }
    public required List<string> Authors { get; set; } = [];
    public required string ISBN { get; init; }
    public required int YearOfRelease { get; set; }
    public required List<string> Genres { get; init; } = [];
    public required bool IsLoan { get; init; }
}