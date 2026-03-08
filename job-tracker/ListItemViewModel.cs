using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive;
using System.Runtime.InteropServices;
using ReactiveUI;

namespace JobTracker;

/// <summary>
/// ViewModel for a single job application entry with reactive property updates.
/// </summary>
public class ListItemViewModel : ReactiveObject
{
    public string Title { get; set; }
    
    private string _node;
    public string Node
    {
        get => _node;
        set => this.RaiseAndSetIfChanged(ref _node, value);
    }

    public ReactiveCommand<Unit, Unit> ButtonCommand { get; }
    
    private bool _isOpen;
    public bool IsOpen
    {
        get => _isOpen;
        set => this.RaiseAndSetIfChanged(ref _isOpen, value);
    }

    public string CreateTime { get; set; } = DateTimeOffset.Now.ToLocalTime().Date.ToString("dd/MM/yyyy");

    public ListItemViewModel(string title, string node, bool isOpen, Action onButtonClick, [Optional] string createTime)
    {
        Title = title;
        Node = node;
        IsOpen = isOpen;
        
        if(!string.IsNullOrEmpty(createTime))
            CreateTime = createTime;
        
        ButtonCommand = ReactiveCommand.Create(onButtonClick);
    }
}

/// <summary>
/// Observable collection that automatically tracks open/closed application counts.
/// </summary>
public class CustomObservableCollection : ObservableCollection<ListItemViewModel>
{
    public int IsOpenCount { get; set; }
    public int IsCloseCount { get; set; }
    
    private void CheckIsOpen()
    {
        IsOpenCount = 0;   
        IsCloseCount = 0;

        foreach (var listItemViewModel in this)
        {
            if (listItemViewModel.IsOpen)
                IsOpenCount++;
            else
                IsCloseCount++;
        }
        
        OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsOpenCount)));
        OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsCloseCount)));
    }
    
    protected override void InsertItem(int index, ListItemViewModel item)
    {
        base.InsertItem(index, item);
        item.PropertyChanged += OnItemPropertyChanged;
        CheckIsOpen();
    }

    protected override void RemoveItem(int index)
    {
        this[index].PropertyChanged -= OnItemPropertyChanged; // Prevent memory leaks
        base.RemoveItem(index);
        CheckIsOpen();
    }

    protected override void ClearItems()
    {
        foreach (var item in this)
            item.PropertyChanged -= OnItemPropertyChanged;
        base.ClearItems();
        CheckIsOpen();
    }

    private void OnItemPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ListItemViewModel.IsOpen))
            CheckIsOpen();
    }
}
