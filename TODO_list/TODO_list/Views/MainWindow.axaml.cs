
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.ReactiveUI;
using ReactiveUI;
using TODO_list.ViewModels;
using Dialogs.Avalonia;
using TODO_list.DB.Models;

namespace TODO_list.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
        this.WhenActivated(d => d(ViewModel!.ShowDialog.RegisterHandler(DoShowDialogAsync)));
        
    }
    
    private async Task DoShowDialogAsync(InteractionContext<AddTaskViewModel, UserTask?> interaction)
    {
       var dialog = new AddTaskWindow();
       dialog.DataContext = interaction.Input;

        var result = await dialog.ShowDialog<UserTask?>(this);
        interaction.SetOutput(result);
    }
    
}