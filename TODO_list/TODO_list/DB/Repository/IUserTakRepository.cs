using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TODO_list.DB.Models;

namespace TODO_list.DB.Repository;

public interface IUserTakRepository
{
    void Create(UserTask entity);
    UserTask? GetOne(int id);
    IEnumerable<UserTask> GetMultiple(
        Expression<Func<UserTask, bool>>? filter = null);

    void Delete(int id);

}