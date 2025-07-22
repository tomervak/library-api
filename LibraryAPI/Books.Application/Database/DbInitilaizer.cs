using Dapper;

namespace Books.Application.Database;

public class DbInitilaizer
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public DbInitilaizer(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task Initialize()
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();

        await connection.ExecuteAsync("""
                                      create table if not exists books (
                                        id UUID primary key,
                                        title TEXT not null,
                                        authors TEXT[] NOT NULL,
                                        isbn TEXT NOT NULL,
                                        year_of_release INT NOT NULL,
                                        genres TEXT[] NOT NULL,
                                        is_loan BOOLEAN NOT NULL);
                                      """);
        
    }
    
    
}