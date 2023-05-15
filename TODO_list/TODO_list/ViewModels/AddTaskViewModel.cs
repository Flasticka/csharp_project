using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using ReactiveUI;
using TODO_list.DB.Models;
using TODO_list.DB.UnitOfWork;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Media;
using ReactiveUI.Fody.Helpers;

namespace TODO_list.ViewModels;

public class AddTaskViewModel : ViewModelBase
{
    private string _taskDescription = "";
    private string _taskCategory = "";
    private Difficulty _taskDifficulty = Difficulty.Easier;
    private string? _taskDeadline;



    [Reactive] public bool AllFilled { get; set; }
    public ReactiveCommand<Unit, UserTask> SubmitDialog { get; }

    public string TaskDescription
    {
        get => _taskDescription;
        set => this.RaiseAndSetIfChanged(ref _taskDescription, value);
    }

    public string TaskDifficulty
    {
        get => _taskDifficulty.ToString();
        set
        {
            Difficulty dificulty = Difficulty.Easier;
            if (value != "")
            {
                dificulty = (Difficulty)Enum.Parse(typeof(Difficulty), value);
            }
            this.RaiseAndSetIfChanged(ref _taskDifficulty, dificulty);
        }
    }

    public string TaskCategory
    {
        get => _taskCategory;
        set => this.RaiseAndSetIfChanged(ref _taskCategory, value);
    }

    public string TaskDeadline
    {
        get => _taskDeadline;
        set => this.RaiseAndSetIfChanged(ref _taskDeadline, value);
    }

    public AddTaskViewModel()
    {   
        this.WhenAnyValue(x => x.TaskDescription, 
                x => x.TaskCategory, 
                x => x.TaskDeadline,
                x => x.TaskDifficulty)
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(async (_) => await UpdateFilled());
        SubmitDialog= ReactiveCommand.CreateFromTask(async () =>
        {
            return new UserTask()
            {
                Description = _taskDescription, Category = _taskCategory, Deadline = DateTime.Parse(_taskDeadline),
                Difficulty = _taskDifficulty
            };
        });
    }
    private Task UpdateFilled()
    {
        return Task.Run(() =>
        {   
            AllFilled = TaskDescription != "" && TaskCategory != "" && TaskDeadline != null;
        });
    }
}