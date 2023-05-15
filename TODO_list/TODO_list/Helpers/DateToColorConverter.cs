using System;

namespace TODO_list.Helpers;

public static class DateToColorConverter
{
    public static string ConvertToString(DateTime deadline)
    {
        return deadline < DateTime.Today ? "Red" : "White";
    }
}