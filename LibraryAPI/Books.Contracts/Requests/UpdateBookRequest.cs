namespace Books.Contracts.Requests;

public class UpdateBookRequest
{
    public required  string Title { get; init; }
    public required IEnumerable<string> Authors { get; init; }
    //[Length(10,13)]
    public required string ISBN { get; init; }
    public required int YearOfRelease { get; init; }
    public required  IEnumerable<string> Genres { get; init; } = [];
    public required bool IsLoan { get; init; }
}