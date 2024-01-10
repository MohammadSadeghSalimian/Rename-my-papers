using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace PaperRename2.Services;

public static class TextExtensions
{
    public static string CleanFileName(this string fileName)
    {
        return Path.GetInvalidFileNameChars()
            .Aggregate(fileName, (current, c) => current.Replace(c.ToString(), string.Empty));
    }
    public static string SimplifyName(this string name)
    {

        var m = Regex.Match(name, @"(\w+\s\w+)");
        if (!m.Success)
        {
            return name;
        }

        var fl = m.Groups[1].Value.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
        var shortName = $"{fl[0].ToUpper()[0]}. {fl[1]}";
        return shortName;

    }
       
}