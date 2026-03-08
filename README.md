# Job Tracker

A cross-platform desktop application for managing job applications, built with Avalonia UI and .NET.

![Platform](https://img.shields.io/badge/platform-Windows%20%7C%20Linux%20%7C%20macOS-blue)
![.NET](https://img.shields.io/badge/.NET-10.0-purple)

## 📋 Overview

Job Tracker is a lightweight desktop application that helps organize and track job applications with persistent CSV storage. Built as a practical solution to manage the job search process efficiently.

## ✨ Features

- **Application Management**: Add, track, and organize job applications
- **Status Tracking**: Toggle between "Open" and "Closed" status with visual indicators
- **Real-time Counters**: Automatic counting of total, open, and closed applications
- **Data Persistence**: Automatic CSV save/load with semicolon escaping
- **Configurable Storage**: Customize CSV file location via settings panel
- **Direct CSV Access**: Open saved data in Excel/LibreOffice with one click
- **Cross-Platform**: Runs on Windows, Linux, and macOS
- **Modern UI**: Clean interface with dark theme

## 🛠️ Technical Stack

- **Framework**: .NET 10.0
- **UI Framework**: Avalonia UI (cross-platform XAML)
- **Architecture**: MVVM (Model-View-ViewModel) pattern
- **Reactive Programming**: ReactiveUI for property change notifications
- **Libraries**:
  - CommunityToolkit.Mvvm
  - DynamicData
  - System.Reactive

## 🏗️ Architecture Highlights

### Custom Observable Collection
Implemented `CustomObservableCollection` that automatically tracks and updates counters in real-time:
- Subscribes to property changes on collection items
- Automatically updates UI counters when application status changes
- Proper memory management with subscribe/unsubscribe pattern

### CSV Handler with Escape Sequences
Robust CSV serialization that handles special characters:
```csharp
// Escape semicolons to prevent CSV parsing issues
e.Title.Replace(";","<DL!#>")
```

### Reactive Property Binding
Efficient property change notifications using ReactiveUI:
```csharp
public bool IsOpen
{
    get => _isOpen;
    set => this.RaiseAndSetIfChanged(ref _isOpen, value);
}
```

### Panel-Based Settings
Clean UI state management without multiple windows:
- Settings panel toggles visibility
- Maintains single-window user experience
- Persistent configuration storage

## 🚀 Getting Started

### Prerequisites
- .NET 10.0 SDK or later
- IDE: Visual Studio, Rider, or VS Code

### Installation

1. Clone the repository:
```bash
git clone https://github.com/Christian-Kammerath/job-tracker.git
cd job-tracker
```

2. Restore dependencies:
```bash
dotnet restore
```

3. Build the project:
```bash
dotnet build
```

4. Run the application:
```bash
dotnet run --project job-tracker/job-tracker.csproj
```

## 📖 Usage

1. **Add Application**: Enter company/position name and click "Speichern"
2. **Add Notes**: Use the dropdown arrow to add detailed notes for each application
3. **Toggle Status**: Click the checkbox to mark applications as open/closed
4. **Remove Application**: Use the delete button (X) on each item
5. **Configure Storage**: Click ⚙️ Settings to change CSV file location
6. **Open in Excel**: Click "Öffne Csv" to view data in your default spreadsheet application
7. **Auto-Save**: All changes are automatically saved to CSV

## 🗂️ Project Structure

```
job-tracker/
├── App.axaml               # Application entry point
├── MainWindow.axaml        # Main window UI definition
├── MainWindow.axaml.cs     # Main window logic
├── ListItemViewModel.cs    # ViewModel + Custom collection
├── CsvHandler.cs           # CSV serialization/deserialization
├── Settings.axaml          # Settings panel UI
├── Settings.axaml.cs       # Settings logic
├── Node.axaml              # Custom control for list items
├── Node.axaml.cs           # Node control logic
├── AppConstants.cs         # Application constants
└── Program.cs              # Application startup
```

## 🎯 Key Learning Outcomes

This project demonstrates:
- Cross-platform desktop development with Avalonia UI
- MVVM architecture implementation
- Reactive programming patterns (ReactiveUI)
- Custom collection implementations with event handling
- Data persistence strategies (CSV with special character handling)
- Memory leak prevention (proper subscribe/unsubscribe)
- UI/UX design for desktop applications
- Settings management and user preferences

## 🔧 Configuration

The CSV file path is configurable through the settings panel (⚙️ button). By default, files are saved to:
```
Documents/ApplicationList/Save.csv
```

## 📝 Development Notes

**Problem Solved**: During my own job search, managing applications in Excel became cumbersome. This tool was born from the need to efficiently track multiple applications with status updates and notes.

**Technical Decisions**:
- **Avalonia over WPF**: Cross-platform compatibility from day one
- **CSV over Database**: Simple, portable, Excel-compatible
- **MVVM Pattern**: Clean separation of concerns, testable code
- **Reactive Properties**: Automatic UI updates without manual event wiring

## 🤝 Contributing

This is a personal project built for learning and practical use. Feedback and suggestions are welcome!

## 📄 License

This project is open source and available under the MIT License.

## 👤 Author

**Christian Kammerath**
- LinkedIn: [linkedin.com/in/Christian-Kammerath](https://linkedin.com/in/Christian-Kammerath)

## 🙏 Acknowledgments

- Built with [Avalonia UI](https://avaloniaui.net/)
- Uses [ReactiveUI](https://www.reactiveui.net/) for reactive programming
- Inspired by the need to organize my own job search

---

*"The best tools are the ones you build yourself."* - Built during my job search journey to practice .NET development and solve a real problem.
