using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using TODO_list.DB.Enums;

namespace TODO_list.Views;

public partial class TodoListView : UserControl
{
    public TodoListView()
    {
        InitializeComponent();
        var comboBoxDifficulty = this.Find<ComboBox>("difficultyComboBox");
        var difficultyValues = Enum.GetValues(typeof(Difficulty))
            .Cast<Difficulty>()
            .Select(v => v.ToString())
            .ToList();
        difficultyValues.Add(string.Empty);
        comboBoxDifficulty.Items = difficultyValues;
        comboBoxDifficulty.SelectedIndex = 0;
        var comboBoxStatus = this.Find<ComboBox>("statusComboBox");
        var statusValues = Enum.GetValues(typeof(TaskStatus))
            .Cast<TaskStatus>()
            .Select(v => v.ToString())
            .ToList();
        statusValues.Add(string.Empty);
        comboBoxStatus.Items = statusValues;
        comboBoxStatus.SelectedIndex = 0;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}