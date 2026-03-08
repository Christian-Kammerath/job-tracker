using System;
using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace JobTracker;

public partial class MainWindow : Window
{
    public CustomObservableCollection Items { get; } = new();
    private CsvHandler _csvHandler = new CsvHandler();
    
    public MainWindow()
    {
        var loadList = _csvHandler.LoadFromCsv();
        
        if(loadList.Count != 0)
            Items = loadList;
 
        InitializeComponent();
        DataContext = this;
        
        Closed += (sender, e) => 
        {
            _csvHandler.SaveInCsv(Items);
        };
    }
    
    private void Button_Save_List_Item(object? sender, RoutedEventArgs e)
    {
        if (!String.IsNullOrEmpty(InputBox.Text))
        {
            string inputText = InputBox.Text;
            ListItemViewModel? vm = null;
            
            vm = new ListItemViewModel(inputText, "", true, () =>
            {
                if (vm != null)
                    Items.Remove(vm); 
            });

            Items.Add(vm);
            InputBox.Text = String.Empty;
            _csvHandler.SaveInCsv(Items);
        }
    }

    private void OpenSettings_OnClick(object? sender, RoutedEventArgs e)
    {
        MainView.IsVisible = false;
        SettingsPanel.IsVisible = true;
    }

    private void CloseSettings_OnClick(object? sender, RoutedEventArgs e)
    {
        SettingsPanel.IsVisible = false;
        MainView.IsVisible = true;
    }

    private void OpenCsv_OnClick(object? sender, RoutedEventArgs e)
    {
        var replaceLineEndings = AppConstants.CsvPath!.ReplaceLineEndings("");
        Process.Start(new ProcessStartInfo
        {
            FileName = replaceLineEndings,
            UseShellExecute = true
        });
    }
}
