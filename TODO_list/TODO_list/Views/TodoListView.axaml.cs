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
        var difficultyValues = Enum.GetValues(typeof(Difficulty))
            .Cast<Difficulty>()
            .Select(v => v.ToString())
            .ToList();
        difficultyValues.Add(string.Empty);
        difficultyComboBox.Items = difficultyValues;
        difficultyComboBox.SelectedIndex = 0;
        var statusComboBox = this.Find<ComboBox>("statusComboBox");
        var statusValues = Enum.GetValues(typeof(TaskStatus))
            .Cast<TaskStatus>()
            .Select(v => v.ToString())
            .ToList();
        statusValues.Add(string.Empty);
        statusComboBox.Items = statusValues;
        statusComboBox.SelectedIndex = 0;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}