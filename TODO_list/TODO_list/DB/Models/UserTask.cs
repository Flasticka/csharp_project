

using System;

namespace TODO_list.DB.Models;

public class UserTask
{
    public int Id { get; set; }
    public string Category { get; set; }
    public string Description { get; set; }
    public int Difficulty { get; set; }
    public DateTime Deadline { get; set; }
    
}