namespace ModernVPN.MVVM.Model;

public class ServerModel
{
    public int ID { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Server { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
}
