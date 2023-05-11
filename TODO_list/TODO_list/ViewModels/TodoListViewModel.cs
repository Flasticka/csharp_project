using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using TODO_list.DB.Models;
using TODO_list.DB.UnitOfWork;
using TODO_list.Views;

namespace TODO_list.ViewModels;

public class TodoListViewModel : ViewModelBase
{
    
    [Reactive]
    public bool Show { get; set; }
    public ICommand AddTaskCommand { get; }
    private IUnitOfWork _unitOfWork;

    public Interaction<AddTaskViewModel, OOTaskWiewModel?> ShowDialog { get; }
    public TodoListViewModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        Items = new ObservableCollection<UserTask>(unitOfWork.UserTaskRepository.GetMultiple());
        ShowDialog = new Interaction<AddTaskViewModel, OOTaskWiewModel?>();

        AddTaskCommand = ReactiveCommand.CreateFromTask(async (MainWindow mainWindow) =>
        {
            var store = new AddTaskViewModel();

            var result = await ShowDialog.Handle(store);
        });

    }

    public ObservableCollection<UserTask> Items { get; }
}