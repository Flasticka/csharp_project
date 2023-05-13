using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TODO_list.DB.Models;

namespace TODO_list.DB;

public class TodoListContext : DbContext
{   
    protected readonly IConfiguration Configuration;
    public TodoListContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        Configuration.GetConnectionString("DatabaseConnectionString");
        options.UseSqlite(Configuration.GetConnectionString("DatabaseConnectionString"));
    }
    public DbSet<UserTask> UserTasks => Set<UserTask>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserTask>()
            .Property(b => b.Status)
            .HasDefaultValue(TaskStatus.Incomplete);
        
    }


}