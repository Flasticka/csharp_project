using System;
using System.Collections.Generic;
using System.Drawing;
using TODO_list.DB.Models;
using TODO_list.Helpers;

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
    public Difficulty UserTaskDifficulty => _userTask.Difficulty;
    public string DifficultyColor => DifficultyToColorConverter.ConvertToString(_userTask.Difficulty);
    public String UserTaskDeadline => _userTask.Deadline.ToString("dd.MM.yyyy");

}