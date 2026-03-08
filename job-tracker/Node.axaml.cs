using Avalonia.Controls;
using Avalonia.Interactivity;


namespace JobTracker;

public partial class Node : UserControl
{
    public Node()
    {
        InitializeComponent();
    }

    private void NoteVisible_OnClick(object? sender, RoutedEventArgs e)
    {
        Button button = (Button)sender!;
        
        TextBox? nodeNox = NoteBox;
        nodeNox.IsVisible = !nodeNox.IsVisible;

        var buttonContent = nodeNox.IsVisible ?  "▲" :  "▼";
        button!.Content = buttonContent;
    }
}