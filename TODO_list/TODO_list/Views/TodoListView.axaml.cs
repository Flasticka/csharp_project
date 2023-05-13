using System;
using System.Linq;
using System.Reactive;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using TODO_list.DB.Models;
using TODO_list.ViewModels;

namespace TODO_list.Views;

public partial class TodoListView : UserControl
{   
    
    public TodoListView()
    {
        InitializeComponent();
        var difficultyComboBox = this.Find<ComboBox>("difficultyComboBox");
        var values = Enum.GetValues(typeof(Difficulty))
            .Cast<Difficulty>()
            .Select(v => v.ToString())
            .ToList();
        values.Add(null);
        difficultyComboBox.Items = values;
        difficultyComboBox.SelectedIndex = 0;
        var statusComboBox = this.Find<ComboBox>("statusComboBox");
        statusComboBox.Items = Enum.GetValues(typeof(TaskStatus))
            .Cast<TaskStatus>()
            .Select(v => v.ToString())
            .ToList();
        statusComboBox.SelectedIndex = 0;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}