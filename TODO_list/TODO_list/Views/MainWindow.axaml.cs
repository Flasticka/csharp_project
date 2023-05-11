
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using ReactiveUI;
using TODO_list.ViewModels;

namespace TODO_list.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
        this.WhenActivated(d => d(ViewModel!.ShowDialog.RegisterHandler(DoShowDialogAsync)));
        
    }
    private async Task DoShowDialogAsync(InteractionContext<TodoListViewModel, AddTaskViewModel?> interaction)
    {
       var dialog = new TodoListWindow();
       dialog.DataContext = interaction.Input;

        var result = await dialog.ShowDialog<AddTaskViewModel?>(this);
        interaction.SetOutput(result);
    }
}