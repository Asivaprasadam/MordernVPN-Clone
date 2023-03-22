namespace ModernVPN.MVVM.ViewModel;

public class GlobalViewModel : ObservableObject
{
    public static GlobalViewModel Instance { get; set; } = new GlobalViewModel();

    private bool _isAwesome;
    public bool IsAwesome
    {
        get => _isAwesome;
        set => SetProperty(ref _isAwesome, value);
    }
}
