using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace TODO_list.Views;

public partial class AddTaskView : UserControl
{
    public AddTaskView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}