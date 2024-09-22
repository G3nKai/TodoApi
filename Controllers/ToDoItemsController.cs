using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[Route("api/[controller]")]
[ApiController]
public class TodoItemsController : ControllerBase
{
    private static List<TodoItem> _todoItems = new List<TodoItem>();

    [HttpGet]
    public ActionResult<List<TodoItem>> GetAll() => _todoItems;

    [HttpPost]
    public IActionResult Create(TodoItem item)
    {
        _todoItems.Add(item);
        return CreatedAtAction(nameof(Create), new { id = item.Id }, item);
    }
}
