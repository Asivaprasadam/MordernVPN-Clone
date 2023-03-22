namespace ModernVPN.MVVM.ViewModel;

public class SettingsViewModel : ObservableObject
{
    public GlobalViewModel Global { get; } = GlobalViewModel.Instance;

    public SettingsViewModel()
    {
    }
}
