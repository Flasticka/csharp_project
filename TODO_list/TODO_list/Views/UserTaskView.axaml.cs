using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace TODO_list.Views;

public partial class UserTaskView : UserControl
{
    public UserTaskView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}