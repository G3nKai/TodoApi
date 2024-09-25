using Microsoft.AspNetCore.Mvc;
using TodoApi.Data;
using TodoApi.Models;
using Microsoft.EntityFrameworkCore;

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
    public async Task<ActionResult<IEnumerable<TodoItem>>> Get()
    {
        return await _context.TodoItems.ToListAsync();
    }
    [HttpPost]
    public async Task<ActionResult<IEnumerable<TodoItem>>> Post(List<PostTodoItem> postItem)
    {
        if (postItem == null)
        {
            return BadRequest();
        }

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
        await _context.SaveChangesAsync();
        return Ok(postItem);
    }

    [HttpDelete("{id}")]

    public async Task<ActionResult<TodoItem>> Delete(int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }
        TodoItem? item = _context.TodoItems.FirstOrDefault(x => x.Id == id);
        if (item == null)
        {
            return NotFound();
        }
        _context.TodoItems.Remove(item);
        await _context.SaveChangesAsync();
        return Ok(item);
    }
    [HttpPatch("{id}")]
    public async Task<ActionResult<TodoItem>> Patch(int id, PutTodoItem item)
    {
        if (id <= 0 || item == null)
        {
            return BadRequest();
        }

        TodoItem? foundItem = await _context.TodoItems.FindAsync(id);

        if (foundItem == null)
        {
            return NotFound();
        }

        foundItem.Duty = item.Duty;

        _context.Update(foundItem);
        await _context.SaveChangesAsync();
        return Ok(foundItem);
    }

    [HttpPatch("{id}/complete")]
    public async Task<ActionResult<TodoItem>> Patch(int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        TodoItem? foundItem = await _context.TodoItems.FindAsync(id);

        if (foundItem == null)
        {
            return NotFound();
        }

        foundItem.IsCompleted = !foundItem.IsCompleted;

        _context.Update(foundItem);
        await _context.SaveChangesAsync();
        return Ok(foundItem);
    }
}
