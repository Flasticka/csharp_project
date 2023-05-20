using System;
using System.Reactive;
using ReactiveUI;
using TODO_list.DB.Models;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ReactiveUI.Fody.Helpers;
using TODO_list.DB.Enums;

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
            Difficulty difficulty = Difficulty.Easier;
            if (value != "")
            {
                difficulty = (Difficulty)Enum.Parse(typeof(Difficulty), value);
            }

            this.RaiseAndSetIfChanged(ref _taskDifficulty, difficulty);
        }
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
        this.WhenAnyValue(x => x.TaskDescription,
                x => x.TaskCategory,
                x => x.TaskDeadline,
                x => x.TaskDifficulty)
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(async (_) => await UpdateFilled());
        SubmitDialog = ReactiveCommand.CreateFromTask(async () => await Task.Run(() => new UserTask()
        {
            Description = _taskDescription, Category = _taskCategory,
            Deadline = _taskDeadline != null ? DateTime.Parse(_taskDeadline) : DateTime.Today,
            Difficulty = _taskDifficulty
        }));
    }

    private async Task UpdateFilled()
    {
        await Task.Run(() => { AllFilled = TaskDescription != "" && TaskCategory != "" && TaskDeadline != null; });
    }
}