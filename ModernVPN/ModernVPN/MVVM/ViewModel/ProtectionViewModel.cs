namespace ModernVPN.MVVM.ViewModel;

public class ProtectionViewModel : ObservableObject
{
    private string _connectionStatus = string.Empty;
    public string ConnectionStatus
    {
        get => _connectionStatus;
        set => SetProperty(ref _connectionStatus, value);
    }

    private ObservableCollection<ServerModel> _servers = new ObservableCollection<ServerModel>();
    public ObservableCollection<ServerModel> Servers
    {
        get => _servers;
        set => SetProperty(ref _servers, value);
    }

    private RelayCommand _connectCommand;
    public RelayCommand ConnectCommand
    {
        get => _connectCommand;
        set => SetProperty(ref _connectCommand, value);
    }

    public GlobalViewModel Global { get; } = GlobalViewModel.Instance;

    public ProtectionViewModel()
    {
        for (int i = 0; i < 10; i++)
        {
            Servers.Add(new ServerModel()
            {
                Country = "USA"
            });
        }

        ConnectCommand = new RelayCommand(o =>
        {
            _ = Task.Run(() =>
            {
                try
                {
                    ConnectionStatus = "Connecting...";
                    var process = new Process();
                    process.StartInfo.FileName = "cmd.exe";
                    process.StartInfo.WorkingDirectory = Environment.CurrentDirectory;
                    process.StartInfo.ArgumentList.Add("/c rasdial MyServer vpnbook dd4e58m /phonebook:./VPN/VPN.pbk");
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.CreateNoWindow = true;

                    process.Start();
                    process.WaitForExit();

                    switch (process.ExitCode)
                    {
                        case 0:
                            Debug.WriteLine("Success!");
                            ConnectionStatus = "Connected!";
                            break;

                        case 691:
                            ConnectionStatus = "Failed...";
                            Debug.WriteLine("Wrong credentials");
                            break;

                        default:
                            /*
                             * Error Codes
                             * __________________________________
                             * https://learn.microsoft.com/en-us/troubleshoot/windows-client/networking/error-codes-for-dial-up-vpn-connection
                             *
                             * https://learn.microsoft.com/en-us/windows/win32/rras/routing-and-remote-access-error-codes
                             */

                            ConnectionStatus = "Unknown!";
                            Console.WriteLine($"Unknown Case Exit code = #{process.ExitCode}");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString() + "Connection Error");
                }
            });
        });
    }

    /// <summary>
    /// Todo: Run once before connecting!
    /// </summary>
    private void ServerBuilder()
    {
        var folderPath = $"{Directory.GetCurrentDirectory()}/VPN";
        var pbkPath = $"{folderPath}/VPN.pbk";
        var address = "us1.vpnbook.com";

        if (!Directory.Exists(folderPath))
            _ = Directory.CreateDirectory(folderPath);

        if (File.Exists(pbkPath))
            _ = MessageBox.Show("Connection already exists!");

        var sb = new StringBuilder();
        _ = sb.AppendLine("[MyServer]");
        _ = sb.AppendLine("MEDIA=rastapi");
        _ = sb.AppendLine("Port=VPN2-0");
        _ = sb.AppendLine("Device=WAN Miniport (IKEv2)");
        _ = sb.AppendLine("DEVICE=vpn");
        _ = sb.AppendLine($"PhoneNumber={address}");
        File.WriteAllText(pbkPath, sb.ToString());
    }
}
