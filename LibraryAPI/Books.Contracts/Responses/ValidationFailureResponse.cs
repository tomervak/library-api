namespace Books.Contracts.Responses;

public class ValidationFailureResponse
{
    public required IEnumerable<ValidationResponse> Errors { get; init; }
}

public class ValidationResponse
{
    public required String PropertyName { get; init; }
    
    public required string Message { get; init; }
}