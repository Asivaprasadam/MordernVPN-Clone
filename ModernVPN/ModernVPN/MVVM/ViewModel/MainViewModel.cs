namespace ModernVPN.MVVM.ViewModel;

public class MainViewModel : ObservableObject
{
    #region Commands

    public RelayCommand MoveWindowCommand { get; set; }
    public RelayCommand MinimizeWindowCommand { get; set; }
    public RelayCommand MaximizeWindowCommand { get; set; }
    public RelayCommand CloseWindowCommand { get; set; }

    public RelayCommand ShowProtectionView { get; set; }
    public RelayCommand ShowSettingsView { get; set; }

    #endregion Commands

    private object? _currentView;
    public object? CurrentView
    {
        get => _currentView;
        set => SetProperty(ref _currentView, value);
    }

    public ProtectionViewModel ProtectionVM { get; set; } = new ProtectionViewModel();
    public SettingsViewModel SettingsVM { get; set; } = new SettingsViewModel();

    public MainViewModel()
    {
        Application.Current.MainWindow.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;

        MoveWindowCommand = new RelayCommand(o => Application.Current.MainWindow.DragMove());
        CloseWindowCommand = new RelayCommand(o => Application.Current.Shutdown());
        MinimizeWindowCommand = new RelayCommand(o => Application.Current.MainWindow.WindowState = WindowState.Minimized);
        MaximizeWindowCommand = new RelayCommand(o =>
        {
            if (Application.Current.MainWindow.WindowState == WindowState.Maximized)
                Application.Current.MainWindow.WindowState = WindowState.Normal;
            else
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
        });

        ProtectionVM = new ProtectionViewModel();
        CurrentView = ProtectionVM;

        ShowProtectionView = new RelayCommand(o => CurrentView = ProtectionVM);
        ShowSettingsView = new RelayCommand(o => CurrentView = SettingsVM);
    }
}
