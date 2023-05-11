using System;
using TODO_list.DB.Repository;

namespace TODO_list.DB.UnitOfWork;

public interface IUnitOfWork: IDisposable
{
    public IUserTakRepository UserTaskRepository { get; }
    void Commit();
}