using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using TODO_list.ViewModels;

namespace TODO_list.Views;

public partial class TodoListView : UserControl
{
    public TodoListView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}