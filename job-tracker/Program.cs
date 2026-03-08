using Avalonia;
using System;
using System.IO;
using Avalonia.ReactiveUI;

namespace JobTracker;

class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        AppConstants.CsvPath = LoadCsvPath("csv_path.txt");
        
        BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
    } 

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace()
            .UseReactiveUI();

    private static string LoadCsvPath(string csvPathSettingFile)
    {
        if (!Path.Exists(csvPathSettingFile))
        {
            using (File.Create(csvPathSettingFile));
        }
        
        string csvSavePath = File.ReadAllText(csvPathSettingFile).ReplaceLineEndings("");
        
        if(string.IsNullOrEmpty(csvSavePath))
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            csvSavePath = Path.Combine(documentsPath, "ApplicationList", "Save.csv");
            File.WriteAllText(csvPathSettingFile, csvSavePath);
        }

        if (!Path.Exists(csvSavePath))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(csvSavePath)!);
            File.Create(csvSavePath).Dispose();
        }
        
        return csvSavePath;
    }
}
