using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TodoApi.Data;

[Route("api/[controller]")]
[ApiController]
public class TodoItemsController : ControllerBase
{
    private readonly TodoContext _context;

    public TodoItemsController(TodoContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<List<TodoItem>> GetAll() => _context.TodoItems.ToList();

    [HttpPost]
    public IActionResult Create(TodoItem item)
    {
        _context.TodoItems.Add(item);
        _context.SaveChanges();
        return CreatedAtAction(nameof(Create), new { id = item.Id }, item);
    }
}