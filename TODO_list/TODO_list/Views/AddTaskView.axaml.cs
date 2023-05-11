using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using TODO_list.ViewModels;

namespace TODO_list.Views;

public partial class AddTaskView : UserControl
{
    public AddTaskView()
    {
        InitializeComponent();
        DataContext = new AddTaskViewModel();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}