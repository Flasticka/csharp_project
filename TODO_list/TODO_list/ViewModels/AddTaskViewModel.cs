using System;
using System.Reactive;
using ReactiveUI;
using TODO_list.DB.Models;
using TODO_list.DB.UnitOfWork;
using System.Reactive;

namespace TODO_list.ViewModels;

public class AddTaskViewModel : ViewModelBase
{
    private string? _taskDescription;
    private string? _taskCategory;
    private int _taskDifficulty;
    private string? _taskDeadline;
    

  

    public ReactiveCommand<Unit, UserTask> SubmitDialog { get; }

    public string? TaskDescription
    {
        get => _taskDescription;
        set => this.RaiseAndSetIfChanged(ref _taskDescription, value);
    }

    public int TaskDifficulty
    {
        get => _taskDifficulty;
        set => this.RaiseAndSetIfChanged(ref _taskDifficulty, value);
    }

    public string? TaskCategory
    {
        get => _taskCategory;
        set => this.RaiseAndSetIfChanged(ref _taskCategory, value);
    }

    public string? TaskDeadline
    {
        get => _taskDeadline;
        set => this.RaiseAndSetIfChanged(ref _taskDeadline, value);
    }

    public AddTaskViewModel()
    {
        SubmitDialog= ReactiveCommand.CreateFromTask(async () => new UserTask()
        {
            Description = TaskDescription, Category = TaskCategory, Deadline = DateTime.Parse(TaskDeadline),
            Difficulty = TaskDifficulty
        });
    }
}