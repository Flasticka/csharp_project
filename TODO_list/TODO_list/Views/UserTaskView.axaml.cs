using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using TODO_list.DB.Models;
using TODO_list.ViewModels;

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