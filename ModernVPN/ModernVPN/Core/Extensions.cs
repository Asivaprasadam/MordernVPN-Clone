namespace ModernVPN.Core;

public class Extensions
{
    public static readonly DependencyProperty Icon = DependencyProperty.RegisterAttached("Icon", typeof(string), typeof(Extensions), new
        PropertyMetadata(default(string)));

    public static string GetIcon(UIElement element) => (string)element.GetValue(Icon);

    public static void SetIcon(UIElement element, string value) => element.SetValue(Icon, value);
}
