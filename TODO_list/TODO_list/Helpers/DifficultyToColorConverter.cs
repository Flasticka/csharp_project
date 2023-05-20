using System.Collections.Generic;
using TODO_list.DB.Enums;

namespace TODO_list.Helpers;

public static class DifficultyToColorConverter
{
    private static readonly Dictionary<Difficulty, string> TaskColors = new Dictionary<Difficulty, string>()
    {
        { Difficulty.Easier, "#47E727" },
        { Difficulty.Easy, "#2C8819" },
        { Difficulty.Medium, "#F0F621" },
        { Difficulty.Hard, "#F37E6E" },
        { Difficulty.Harder, "#FC0A03" }
    };

    public static string ConvertToString(Difficulty difficulty)
    {
        return TaskColors[difficulty];
    }
}