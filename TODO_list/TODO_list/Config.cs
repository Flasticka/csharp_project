using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace TODO_list;

public static class Config
{
    public static readonly IConfigurationRoot PathConfig = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .Build();
}