using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TODO_list.DB.Models;

namespace TODO_list.DB.Repository;

public class UserTaskRepository : IUserTakRepository
{   
    private readonly TodoListContext _context;

    public UserTaskRepository(TodoListContext context)
    {
        _context = context;
    }

    private DbSet<UserTask> DbSet => _context.UserTasks;

    public void Create(UserTask entity)
    {
        DbSet.Add(entity);
    }

    public UserTask? GetOne(int id)
    {
        return DbSet.Find(id);
    }

    public IEnumerable<UserTask> GetMultiple(Expression<Func<UserTask, bool>>? filter = null)
    {
        if (filter != null)
        {
            return DbSet.Where(filter);
        }
        return DbSet;
    }

    public void Delete(int id)
    {
        var entity = DbSet.Find(id);

        if (entity == null)
        {
            return;
        }

        DbSet.Remove(entity);

    }
}