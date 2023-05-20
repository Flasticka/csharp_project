using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TODO_list.DB.Enums;
using TODO_list.DB.Models;

namespace TODO_list.DB;

public class TodoListContext : DbContext
{
    private readonly IConfiguration _configuration;

    public TodoListContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        _configuration.GetConnectionString("DatabaseConnectionString");
        options.UseSqlite(_configuration.GetConnectionString("DatabaseConnectionString"));
    }

    public DbSet<UserTask> UserTasks => Set<UserTask>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserTask>()
            .Property(b => b.Status)
            .HasDefaultValue(TaskStatus.Incomplete);
    }
}