
using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using TODO_list.DB;
using TODO_list.DB.Models;
using TODO_list.DB.Repository;
using TODO_list.DB.UnitOfWork;
using TODO_list.Views;

namespace TODO_list.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public ICommand AddTaskCommand { get; }
    private IUnitOfWork _unitOfWork;

    public Interaction<TodoListViewModel, AddTaskViewModel?> ShowDialog { get; }
    public MainWindowViewModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        Items = new ObservableCollection<UserTask>(unitOfWork.UserTaskRepository.GetMultiple());
        ShowDialog = new Interaction<TodoListViewModel, AddTaskViewModel?>();

        AddTaskCommand = ReactiveCommand.CreateFromTask(async (MainWindow mainWindow) =>
        {   
            _unitOfWork.UserTaskRepository.Create(new UserTask() { Description = "efe", Category = "effe", Deadline = DateTime.Now, Difficulty = 5});
            _unitOfWork.Commit();
            var todoList = new TodoListViewModel(unitOfWork);
            Items = new ObservableCollection<UserTask>(unitOfWork.UserTaskRepository.GetMultiple());
            var result = await ShowDialog.Handle(todoList);
        });

    }
    
    [Reactive]
    public ObservableCollection<UserTask> Items { get; set; }
    
}