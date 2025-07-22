using Books.Application.Database;
using Books.Application.Models;
using Dapper;

namespace Books.Application.Repositories;


public class BookRepository : IBookRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public BookRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<bool> CreateBookAsync(Book book)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        using var transaction = connection.BeginTransaction();
        
        var result = await connection.ExecuteAsync(
            new CommandDefinition(
                """
                INSERT INTO books (id, title, authors, isbn, year_of_release, genres, is_loan)
                VALUES (@Id, @Title, @Authors, @ISBN, @YearOfRelease, @Genres, @IsLoan)
                """,
                book
            )
        );

        transaction.Commit();
        return result > 0;
    }

    public async Task<Book?> GetByIdAsync(Guid id)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        var book = await connection.QuerySingleOrDefaultAsync<Book>(
            new CommandDefinition("""
                                  select * from books where id = @id
                                  """, new { id }));
        if (book == null)
            return null;
        return book;
    }

    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        var books = await connection.QueryAsync<Book>(new CommandDefinition("select * from books "));
        return books;
    }

    public async Task<bool> UpdateBookAsync(Book book)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        using var transaction = connection.BeginTransaction();

        var result = await connection.ExecuteAsync
        (new CommandDefinition("""
        update books set title = @Title, authors = @Authors, isbn= @ISBN, year_of_release= @YearOfRelease, genres= @Genres, is_loan= @IsLoan where id =@Id
        """, book));
        transaction.Commit();
        return result > 0;
    }

    public async Task<bool> DeleteBookAsync(Guid id)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        using var transaction = connection.BeginTransaction();

        var result = await connection.ExecuteAsync(
            new CommandDefinition(""" 
                                  delete from books where id = @Id
                                  """,new { id }));
        transaction.Commit();
        return result > 0;
    }

    public async Task<bool> ExistByIdAsync(Guid id)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        return await connection.ExecuteScalarAsync<bool>(new CommandDefinition("""
                                                                                           select count(1) from books where id = @id
                                                                                           """, new { id }));
    }

    public async Task<Book?> GetByIsbnAsync(string isbn)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        var book = await connection.QuerySingleOrDefaultAsync<Book>(
            new CommandDefinition("""
                                  select * from books where isbn = @isbn
                                  """, new { isbn }));
        if (book == null)
            return null;
        return book;
    }
}