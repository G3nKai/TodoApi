using Microsoft.AspNetCore.Mvc;
using TodoApi.Data;
using TodoApi.Models;

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
    public IActionResult Create(List<PostTodoItem> postItem)
    {
        List<TodoItem> items = new List<TodoItem>();

        foreach (PostTodoItem it in postItem)
        {  
            var newItem = new TodoItem
            {
                Id = 0,
                Duty = it.Duty,
                IsCompleted = it.IsCompleted
            };
            items.Add(newItem);
        }
        _context.TodoItems.AddRange(items);
        _context.SaveChanges();
        return CreatedAtAction(nameof(Create), items);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var item = _context.TodoItems.Find(id);
        if (item == null) return NotFound();

        _context.TodoItems.Remove(item);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, TodoItem updatedItem)
    {
        var item = _context.TodoItems.Find(id);
        if (item == null) return NotFound();

        item.Duty = updatedItem.Duty;
        item.IsCompleted = updatedItem.IsCompleted;
        _context.SaveChanges();
        return NoContent();
    }

    [HttpPatch("{id}/complete")]
    public IActionResult Complete(int id)
    {
        var item = _context.TodoItems.Find(id);
        if (item == null) return NotFound();

        item.IsCompleted = !item.IsCompleted;
        _context.SaveChanges();
        return NoContent();
    }
}
