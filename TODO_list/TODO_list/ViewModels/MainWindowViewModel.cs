using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using TODO_list.DB.Enums;
using TODO_list.DB.Models;
using TODO_list.DB.UnitOfWork;
using TaskStatus = TODO_list.DB.Enums.TaskStatus;

namespace TODO_list.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public ICommand AddTaskCommand { get; }
    public ICommand DeleteTaskCommand { get; }
    public ICommand MarkCompletedCommand { get; }
    public ICommand ExitAppCommand { get; }
    public ICommand ResetFilterCommand { get; }

    private readonly IUnitOfWork _unitOfWork;
    private UserTaskViewModel? _selectedUserTask;
    private string _nameFilter = "";
    private string _categoryFilter = "";
    private string? _fromFilter;
    private string? _toFilter;
    private Difficulty? _difficultyFilter;
    private TaskStatus? _statusFilter;
    public Interaction<AddTaskViewModel, UserTask?> ShowDialog { get; }

    [Reactive]
    public ObservableCollection<UserTaskViewModel> Items { get; set; } = new ObservableCollection<UserTaskViewModel>();

    [Reactive] public bool EnableDelete { get; set; }
    [Reactive] public bool EnableComplete { get; set; }
    [Reactive] public bool Initilized { get; set; }

    public UserTaskViewModel? SelectedUserTask
    {
        get => _selectedUserTask;
        set
        {
            EnableDelete = value != null;
            EnableComplete = value is { UserStatus: TaskStatus.Incomplete };
            this.RaiseAndSetIfChanged(ref _selectedUserTask, value);
        }
    }

    public string NameFilter
    {
        get => _nameFilter;
        set => this.RaiseAndSetIfChanged(ref _nameFilter, value);
    }

    public string CategoryFilter
    {
        get => _categoryFilter;
        set => this.RaiseAndSetIfChanged(ref _categoryFilter, value);
    }

    public string? FromFilter
    {
        get => _fromFilter;
        set => this.RaiseAndSetIfChanged(ref _fromFilter, value);
    }

    public string? ToFilter
    {
        get => _toFilter;
        set => this.RaiseAndSetIfChanged(ref _toFilter, value);
    }

    public string DifficultyFilter
    {
        get => _difficultyFilter?.ToString() ?? string.Empty;
        set
        {
            Difficulty? difficulty = null;
            if (value != "")
            {
                difficulty = (Difficulty)Enum.Parse(typeof(Difficulty), value);
            }

            this.RaiseAndSetIfChanged(ref _difficultyFilter, difficulty);
        }
    }

    public string StatusFilter
    {
        get => _statusFilter?.ToString() ?? string.Empty;
        set
        {
            TaskStatus? taskStatus = null;
            if (value != "")
            {
                taskStatus = (TaskStatus)Enum.Parse(typeof(TaskStatus), value);
            }

            this.RaiseAndSetIfChanged(ref _statusFilter, taskStatus);
        }
    }

    public MainWindowViewModel(IUnitOfWork unitOfWork)
    {
        this.WhenAnyValue(x => x.NameFilter,
                x => x.CategoryFilter,
                x => x.DifficultyFilter,
                x => x.StatusFilter,
                x => x.FromFilter,
                x => x.ToFilter,
                x => x.Initilized
            )
            .Throttle(TimeSpan.FromMilliseconds(400))
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe((_) => UpdateUserTasks());
        _unitOfWork = unitOfWork;
        UpdateUserTasks();
        ShowDialog = new Interaction<AddTaskViewModel, UserTask?>();

        AddTaskCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var addTask = new AddTaskViewModel();
                var result = await ShowDialog.Handle(addTask);
                if (result != null)
                {
                    _unitOfWork.UserTaskRepository.Create(result);
                    _unitOfWork.Commit();
                }

                await UpdateUserTasks();
            }
        );
        DeleteTaskCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                if (SelectedUserTask != null)
                {
                    _unitOfWork.UserTaskRepository.Delete(SelectedUserTask.UserTaskId);
                    _unitOfWork.Commit();
                }

                await UpdateUserTasks();
            }
        );
        ExitAppCommand = ReactiveCommand.CreateFromTask(
            () => Task.Run(() => System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow())
        );
        MarkCompletedCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                if (SelectedUserTask != null)
                {
                    _unitOfWork.UserTaskRepository.MarkAsComplete(SelectedUserTask.UserTaskId);
                    _unitOfWork.Commit();
                }

                await UpdateUserTasks();
            }
        );
        ResetFilterCommand = ReactiveCommand.CreateFromTask(() => Task.Run(() =>
            {
                NameFilter = "";
                CategoryFilter = "";
                DifficultyFilter = "";
                FromFilter = null;
                ToFilter = null;
                StatusFilter = "";
            })
        );
    }

    private Task UpdateUserTasks()
    {
        return Task.Run(() =>
        {
            Items = new ObservableCollection<UserTaskViewModel>(_unitOfWork.UserTaskRepository
                .GetMultiple((task) => task.Description.Contains(_nameFilter) &&
                                       task.Category.Contains(_categoryFilter) &&
                                       (_fromFilter == null || task.Deadline >= DateTime.Parse(_fromFilter)) &&
                                       (_toFilter == null || task.Deadline <= DateTime.Parse(_toFilter)) &&
                                       (_difficultyFilter == null || task.Difficulty.Equals(_difficultyFilter)) &&
                                       (_statusFilter == null || task.Status.Equals(_statusFilter)))
                .Select(item => new
                {
                    UserTaks = item,
                    TaskPriority = ComputeTaskPriority(item),
                }).OrderByDescending(arg => arg.TaskPriority)
                .Select(item => new UserTaskViewModel(item.UserTaks)));
        });
    }

    private double ComputeTaskPriority(UserTask userTask)
    {
        if (userTask.Status == TaskStatus.Complete)
        {
            return 0;
        }

        if (userTask.Deadline <= DateTime.Today)
        {
            return (double)userTask.Difficulty +
                   (((DateTime.Today - userTask.Deadline).Days + 1) * ((double)Difficulty.Harder + 1));
        }

        return (double)userTask.Difficulty / (userTask.Deadline - DateTime.Today).Days;
    }
}