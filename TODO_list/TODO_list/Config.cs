using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace TODO_list;

public static class Config
{
    public static IConfigurationRoot config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .Build();
}