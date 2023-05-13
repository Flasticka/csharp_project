using Microsoft.Extensions.Configuration;

using TODO_list.DB.Repository;

namespace TODO_list.DB.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{   
    private readonly TodoListContext _dbContext;
    public UnitOfWork()
    {
        
        _dbContext = new TodoListContext( Config.config);
        UserTaskRepository = new UserTaskRepository(_dbContext);
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }

    public IUserTakRepository UserTaskRepository { get; }
    
    public void Commit()
    {
        _dbContext.SaveChanges();
    }
}