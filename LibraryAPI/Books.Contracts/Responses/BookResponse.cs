namespace Books.Contracts.Responses;

public class BookResponse
{
    public required Guid Id { get; init; }
    public required  string Title { get; init; }
    public required IEnumerable<string> Authors { get; init; }
    public required string ISBN { get; init; }
    public required int YearOfRelease { get; init; }
    public required IEnumerable<string> Genres { get; init; } = [];
    public required bool IsLoan { get; init; }

}