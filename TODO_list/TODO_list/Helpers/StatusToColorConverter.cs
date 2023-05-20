using System.Collections.Generic;
using TODO_list.DB.Enums;


namespace TODO_list.Helpers;

public static class StatusToColorConverter
{
    private static readonly Dictionary<TaskStatus, string> TaskColors = new Dictionary<TaskStatus, string>()
    {
        { TaskStatus.Complete, "#47E727" },
        { TaskStatus.Incomplete, "#FC0A03" }
    };

    public static string ConvertToString(TaskStatus status)
    {
        return TaskColors[status];
    }
}