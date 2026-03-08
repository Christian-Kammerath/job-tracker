using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace JobTracker;

/// <summary>
/// Handles CSV persistence for job application data with semicolon escaping.
/// </summary>
public class CsvHandler
{
    private string FictitiousDelimiter { get;  } = "<DL!#>";
    
    private List<string> ObjectListToStringList(ObservableCollection<ListItemViewModel> list)
    {
        return  list.Select(e =>
            $"{e.Title.Replace(";",FictitiousDelimiter)};{(e.IsOpen ? "open" : "close")};{e.Node.Replace(";",FictitiousDelimiter)};{e.CreateTime}").ToList();
    }

    public void SaveInCsv(ObservableCollection<ListItemViewModel> list)
    {
        var csvPreparedList = ObjectListToStringList(list);
        if (AppConstants.CsvPath != null)
        {
            var replaceLineEndings = AppConstants.CsvPath.ReplaceLineEndings("");
            File.WriteAllLines(replaceLineEndings, csvPreparedList);
        }
    }

    public CustomObservableCollection LoadFromCsv()
    {
        var observableList = new CustomObservableCollection();

        if (AppConstants.CsvPath != null)
        {
            var replaceLineEndings = AppConstants.CsvPath.ReplaceLineEndings("");
            var lines = File.ReadAllLines(replaceLineEndings);

            foreach (var line in lines)
            {
                var splitString = line.Split(';');
                ListItemViewModel? vm = null;

                vm = new ListItemViewModel(
                    splitString[0].Replace(FictitiousDelimiter,";"),
                    splitString[2].Replace(FictitiousDelimiter,";"),
                    splitString[1] == "open", () =>
                {
                    if (vm != null)
                        observableList.Remove(vm);
                    
                }, splitString[3]);
                observableList.Add(vm);
            }
        }

        return observableList;
    }
}
