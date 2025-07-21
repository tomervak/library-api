using Books.API.Mapping;
using Books.Application.Models;
using Books.Application.Repositories;
using Books.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Books.API.Controllers;

[ApiController]
public class BookController : Controller
{
    private readonly IBookRepository _bookRepository;

    public BookController(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    [HttpGet(ApiEndpoints.Books.Get)]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book == null)
            return NotFound();
        var response = book.MapToBookResponse();
        return Ok(response);
    }

    [HttpGet(ApiEndpoints.Books.GetAll)]
    public async Task<IActionResult> GetAll()
    {
        var books =await _bookRepository.GetAllAsync();
        var response = books.Select((book) => book.MapToBookResponse());
        return Ok(response);
    }
    
    [HttpPost(ApiEndpoints.Books.Create)]
    public async Task<IActionResult> Create([FromBody] CreateBookRequest request)
    {
        var newBook = request.MapToNewBook();
        var result = await _bookRepository.CreateBookAsync(newBook);
        return Created($"/{ApiEndpoints.Books.Create}/{newBook.Id}", newBook);
    }

    [HttpPut(ApiEndpoints.Books.Update)]
    public async Task<IActionResult> Update([FromRoute] Guid id, UpdateBookRequest request)
    {
        var book = request.MapToBook(id);
        var update = await _bookRepository.UpdateBookAsync(book);
        if (!update)
        {
            return NotFound();
        }
        var response = book.MapToBookResponse();
        return Ok(response);
    }

    [HttpDelete(ApiEndpoints.Books.Delete)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        bool result = await _bookRepository.DeleteBookAsync(id);
        if (!result)
            return NotFound();
        return Ok();
    }
}