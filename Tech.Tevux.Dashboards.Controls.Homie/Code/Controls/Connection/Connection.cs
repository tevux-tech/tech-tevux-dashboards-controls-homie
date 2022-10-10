using DevBot9.Mvvm;
using NLog;
using System.Threading.Tasks;


namespace Tech.Tevux.Dashboards.Controls.Homie;

[ConnectionProvider]
public partial class Connection : ControlBase, IConnection {
    readonly HomieWatcher _connectionProvider;
    private bool _isDisposed = false;
    public Connection() {
        _connectionProvider = HomieWatcher.Instance;

        ConnectCommand = new AsyncCommand(() => Task.Factory.StartNew(Connect), CanConnectExecute);
        DisconnectCommand = new AsyncCommand(() => Task.Factory.StartNew(Disconnect), CanDisconnectExecute);
    }

    public Logger Log { get; } = LogManager.GetCurrentClassLogger();

    public string ConnectionString => throw new NotImplementedException();

    public string Status => throw new NotImplementedException();

    public bool Connect(string connectionString, out string errorMessage) {
        throw new NotImplementedException();
    }

    public void Initialize() {
        LibrarySupervisor.Instance.GetFromCache(this, nameof(LastUsedConnectionString), out var value);
        if (value is string goodString) {
            IpAddress = goodString;
            LastUsedConnectionString = goodString;
        }
    }

    public override void OnApplyTemplate() {
        base.OnApplyTemplate();

        if (DesignerProperties.GetIsInDesignMode(this)) { return; }

        Initialize();
    }

    protected override void Dispose(bool isCalledManually) {
        if (_isDisposed == false) {
            if (isCalledManually) {
                // Dispose managed objects here.
            }

            // Free unmanaged resources here and set large fields to null.

            _isDisposed = true;
        }
    }

    private bool CanConnectExecute() {
        if (_connectionProvider.IsConnected) { return false; }

        return true;
    }

    private bool CanDisconnectExecute() {
        if (_connectionProvider.IsConnected) { return true; }

        return false;
    }

    private void Connect() {
        var localIpAddress = "";
        var localConnectionString = "";

        Dispatcher.Invoke(() => {
            localIpAddress = IpAddress;
            localConnectionString = LastUsedConnectionString;
        });

        UpdateStatusCode(3, $"VirtualDevice: trying to connect to {localIpAddress} ...");
        _connectionProvider.Connect(localIpAddress);

        if (_connectionProvider.IsConnected) {

            // User might have changed IP address and things before connecting. If so, we need to save new settings.
            if (localConnectionString != localIpAddress) {
                Dispatcher.Invoke(() => {
                    LastUsedConnectionString = IpAddress;
                    LibrarySupervisor.Instance.SetToCache(this, nameof(LastUsedConnectionString), LastUsedConnectionString);
                });
            }
            UpdateStatusCode(2, $"Connected to {localIpAddress}");
        } else {
            UpdateStatusCode(1, "Could not connect.");
        }
    }

    private void Disconnect() {
        _connectionProvider.Disconnect();
        UpdateStatusCode(0, "Disconnected by user.");
    }

    void IConnection.Disconnect() {
        throw new NotImplementedException();
    }

    private void UpdateStatusCode(int code, string message) {
        Application.Current.Dispatcher.Invoke(() => {
            StatusCode = code;
            StatusMessage = message;
            ConnectCommand.CanExecute(null);
            DisconnectCommand.CanExecute(null);
        });
    }
}
