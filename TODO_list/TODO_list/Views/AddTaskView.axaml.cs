using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using TODO_list.DB.Enums;
using TODO_list.DB.Models;
using TODO_list.ViewModels;

namespace TODO_list.Views;

public partial class AddTaskView : UserControl
{
    public AddTaskView()
    {
        InitializeComponent();
        var comboBoxDifficulty = this.Find<ComboBox>("difficultyComboBox");
        comboBoxDifficulty.Items = Enum.GetValues(typeof(Difficulty))
            .Cast<Difficulty>()
            .Select(v => v.ToString())
            .ToList();
        comboBoxDifficulty.SelectedItem = Difficulty.Easier;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}