using System;
using Humanizer;

namespace PaperRename2.Services;

public static class PaperInformationExtension
{
    public static void AddEtAl(this IPaperModel paperModel)
    {
        paperModel.Author = $"{paperModel.Author} et al";
    }
    public static void SimplifyAuthor(this IPaperModel paperModel)
    {
        paperModel.Author = paperModel.Author.SimplifyName();
    }
    public static void Normalize(this IPaperModel paperModel)
    {
        paperModel.Title = paperModel.Title.ToLower().Replace(Environment.NewLine, " ").Titleize().CleanFileName();
        paperModel.Author = paperModel.Author.ToLower().Replace(Environment.NewLine, " ").Titleize()
            .CleanFileName();
    }
    public static void GetFileName(this IPaperModel paperModel)
    {

        paperModel.Name= $"{paperModel.Year}-{paperModel.Author.Trim()}-{paperModel.Title.Trim()}.pdf";
        paperModel.Name = paperModel.Name.CleanFileName().Trim();

    }
}