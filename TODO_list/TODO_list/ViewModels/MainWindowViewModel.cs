using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.X11;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ShimSkiaSharp;
using TODO_list.DB;
using TODO_list.DB.Models;
using TODO_list.DB.Repository;
using TODO_list.DB.UnitOfWork;
using TODO_list.Views;
using TaskStatus = TODO_list.DB.Models.TaskStatus;

namespace TODO_list.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public ICommand AddTaskCommand { get; }
    public ICommand DeleteTaskCommand { get; }
    public ICommand MarkCompletedCommand { get; }
    public ICommand ExitAppCommand { get; }

    private IUnitOfWork _unitOfWork;
    private UserTaskViewModel? _selectedUserTask;
    private string _nameFilter = "";
    private string _categoryFilter = "";
    private Difficulty? _difficultyFilter;
    private TaskStatus? _statusFilter;
    public Interaction<AddTaskViewModel, UserTask?> ShowDialog { get; }

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

    public Difficulty? DifficultyFilter
    {
        get => _difficultyFilter;
        set => this.RaiseAndSetIfChanged(ref _difficultyFilter, value);
    }

    public TaskStatus? StatusFilter
    {
        get => _statusFilter;
        set => this.RaiseAndSetIfChanged(ref _statusFilter, value);
    }

    public ReactiveCommand<Unit, Task> FilterCommand { get; }

    public MainWindowViewModel(IUnitOfWork unitOfWork)
    {
        this.WhenAnyValue(x => x.NameFilter, 
                x => x.CategoryFilter, 
                x => x.DifficultyFilter,
                x => x.StatusFilter)
            .Throttle(TimeSpan.FromMilliseconds(400))
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe((_) => UpdateUserTasks());
        _unitOfWork = unitOfWork;
        Items = new ObservableCollection<UserTaskViewModel>(unitOfWork.UserTaskRepository.GetMultiple()
            .Select(item => new UserTaskViewModel(item)));
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
        });
        DeleteTaskCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            if (SelectedUserTask != null)
            {
                _unitOfWork.UserTaskRepository.Delete(SelectedUserTask.UserTaskId);
                _unitOfWork.Commit();
            }

            await UpdateUserTasks();
        });
        ExitAppCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
        });
        MarkCompletedCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            if (SelectedUserTask != null)
            {
                _unitOfWork.UserTaskRepository.MarkAsComplete(SelectedUserTask.UserTaskId);
                _unitOfWork.Commit();
            }

            await UpdateUserTasks();
        });
    }

    [Reactive] public ObservableCollection<UserTaskViewModel> Items { get; set; }
    [Reactive] public bool EnableDelete { get; set; }
    [Reactive] public bool EnableComplete { get; set; }

    private Task UpdateUserTasks()
    {
        return Task.Run(() =>
        {
            Items = new ObservableCollection<UserTaskViewModel>(_unitOfWork.UserTaskRepository
                .GetMultiple((task) => task.Description.Contains(NameFilter) && task.Category.Contains(CategoryFilter) &&
                                       (DifficultyFilter == null || task.Difficulty.Equals(DifficultyFilter)) &&
                                       (StatusFilter == null || task.Status.Equals(StatusFilter)))
                .Select(item => new UserTaskViewModel(item)));
        });
    }
}