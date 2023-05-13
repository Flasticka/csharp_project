using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using ReactiveUI;
using TODO_list.DB.Models;
using TODO_list.DB.UnitOfWork;
using System.Reactive;
using Avalonia.Controls;
using Avalonia.Media;

namespace TODO_list.ViewModels;

public class AddTaskViewModel : ViewModelBase
{
    private string? _taskDescription;
    private string _taskCategory;
    private Difficulty _taskDifficulty;
    private string? _taskDeadline;




    public ReactiveCommand<Unit, UserTask> SubmitDialog { get; }

    public string? TaskDescription
    {
        get => _taskDescription;
        set => this.RaiseAndSetIfChanged(ref _taskDescription, value);
    }
    
    public Difficulty TaskDifficulty
    {
        get => _taskDifficulty;
        set => this.RaiseAndSetIfChanged(ref _taskDifficulty, value);
    }

    public string TaskCategory
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