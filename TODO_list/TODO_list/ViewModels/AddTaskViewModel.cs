using ReactiveUI;

namespace TODO_list.ViewModels;

public class AddTaskViewModel: ViewModelBase
{
    private bool _isBusy;
    private string? _taskDescription;

    public string? TaskDescription
    {
        get => _taskDescription;
        set => this.RaiseAndSetIfChanged(ref _taskDescription, value);
    }

    public bool IsBusy
    {
        get => _isBusy;
        set => this.RaiseAndSetIfChanged(ref _isBusy, value);
    }
}