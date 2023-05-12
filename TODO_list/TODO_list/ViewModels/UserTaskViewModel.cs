using System;
using TODO_list.DB.Models;

namespace TODO_list.ViewModels;

public class UserTaskViewModel
{
    private readonly UserTask _userTask;
    public UserTaskViewModel(UserTask userTask)
    {
        _userTask = userTask;
    }
    
    public int UserTaskId => _userTask.Id;
    public string UserTaskDescription => _userTask.Description;
    public string UserTaskCategory => _userTask.Category;
    public int UserTaskDifficulty => _userTask.Difficulty;
    public DateTime UserTaskDeadline => _userTask.Deadline;

}