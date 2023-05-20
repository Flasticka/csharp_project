using System.Reactive;
using Avalonia;
using Avalonia.Controls.Mixins;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using TODO_list.DB.Models;
using TODO_list.ViewModels;

namespace TODO_list.Views;

public partial class AddTaskWindow : ReactiveWindow<AddTaskViewModel>
{
    public AddTaskWindow()
    {
        InitializeComponent();
        this.WhenActivated(disposables =>
        {
            ViewModel!.SubmitDialog
                .Subscribe(Observer.Create<UserTask?>(Close))
                .DisposeWith(disposables);
        });

#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}