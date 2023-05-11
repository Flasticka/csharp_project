using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using TODO_list.DB;
using TODO_list.DB.UnitOfWork;
using TODO_list.ViewModels;
using TODO_list.Views;

namespace TODO_list;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        using var db = new TodoListContext(Config.config);
        db.Database.EnsureCreated();
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(new UnitOfWork()),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}