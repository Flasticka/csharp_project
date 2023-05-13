using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using TODO_list.DB.Models;
using TODO_list.ViewModels;

namespace TODO_list.Views;

public partial class AddTaskView : UserControl
{
    public AddTaskView()
    {
        InitializeComponent();
        var difficultyComboBox = this.Find<ComboBox>("difficultyComboBox");
        difficultyComboBox.Items = Enum.GetValues(typeof(Difficulty))
            .Cast<Difficulty>()
            .Select(v => v.ToString())
            .ToList();
        difficultyComboBox.SelectedIndex = 0;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}