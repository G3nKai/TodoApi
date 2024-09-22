using System.ComponentModel.DataAnnotations;
using System;
public class TodoItem
{
    [Key]
    public int Id { get; set; }
    public required string Duty { get; set; }
    public bool IsCompleted { get; set; }
}
