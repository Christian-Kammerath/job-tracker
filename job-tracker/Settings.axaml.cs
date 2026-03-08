using System.IO;
using Avalonia.Controls;

namespace JobTracker;

public partial class Settings : UserControl
{
    public Settings()
    {
        InitializeComponent();
        LoadSettings();
    }

    private void LoadSettings()
    {
        CsvBox.Text = AppConstants.CsvPath;
    }

    private void Setting_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        AppConstants.CsvPath = CsvBox.Text;
        SaveSettings();
    }

    private void SaveSettings()
    {
        using (StreamWriter sw = new StreamWriter("csv_path.txt"))
        {
            sw.WriteLine(AppConstants.CsvPath);
        }
    }
}
