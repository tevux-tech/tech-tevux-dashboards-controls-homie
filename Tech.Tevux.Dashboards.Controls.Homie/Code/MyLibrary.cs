using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;
using Tevux.Dashboards.Abstractions;

namespace Tech.Tevux.Dashboards.Controls.Homie;

public class MyLibrary : ILibrary,
                         ISharedLibraryMessengerConsumer,
                         IDashboardControlProvider,
                         IDashboardControlEditorProvider,
                         ICacheConsumer,
                         IConnectionProvider,
                         IConnectionGuiProvider,
                         INotifyPropertyChanged {

    private HomieWatcher _homieProvider;
    private bool _isInitialized;
    private MyLibrary() {
        AvailableConnections = _realCollection;
    }

    public static MyLibrary Instance { get; } = new MyLibrary();
    public ICache Cache { get; private set; } = new EmptyCache();
    public Dictionary<System.Type, List<System.Type>> DashboardControlEditors { get; private set; } = null!;
    public List<System.Type> DashboardControls { get; private set; } = null!;
    public ISharedLibraryMessenger GlobalMessenger { get; private set; } = new EmptyLibraryMessenger();

    #region ILibrary
    private bool _isDisposed;

    public void Dispose() {
        // A good article explaining how to implement Dispose. https://docs.microsoft.com/en-us/dotnet/standard/garbage-collection/implementing-dispose
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void Initialize() {
        if (_isInitialized) { return; }

        _homieProvider = HomieWatcher.Instance;

        _realCollection.CollectionChanged += (sender, e) => {
            Cache.Write(this, nameof(AvailableConnections), AvailableConnections);
        };

        if (Cache.TryRead(this, nameof(AvailableConnections), out var value)) {
            var foundConnectionDefinitions = JsonConvert.DeserializeObject<List<ConnectionDefinition>>(value.ToString());

            if (foundConnectionDefinitions.Count > 0) {
                // User has something defined in the file.
                foreach (var connection in foundConnectionDefinitions) {
                    _realCollection.Add(connection);
                }

            } else {
                // There nothing in the file, so adding default options.
                _realCollection.Add(new ConnectionDefinition() { Name = "default", Parameters = "127.0.0.1" });
                Cache.Write(this, nameof(AvailableConnections), AvailableConnections);
            }
        } else {
            // There nothing in the file, so adding default options.
            _realCollection.Add(new ConnectionDefinition() { Name = "default", Parameters = "127.0.0.1" });
            Cache.Write(this, nameof(AvailableConnections), AvailableConnections);
        }

        // Trying to load last used connection.
        if (Cache.TryRead(this, nameof(CurrentConnection), out var name)) {
            if (_realCollection.Any(c => c.Name == name.ToString())) {
                // Found one, selecting it.
                CurrentConnection = _realCollection.Single(b => b.Name == name.ToString());
            }
        };

        if (CurrentConnection is null) {
            // Selecting first available connection, if any.
            if (_realCollection.Count > 0) {
                CurrentConnection = _realCollection[0];
            }
        }

        DashboardControls = new List<System.Type> {
            typeof(Button),
            typeof(DeviceStatus),
            typeof(Indicator)
        };

        DashboardControlEditors = new Dictionary<System.Type, List<System.Type>>();
        DashboardControlEditors.Add(typeof(Indicator), new List<System.Type>() { typeof(RulesEditor) });
        DashboardControlEditors.Add(typeof(DeviceStatus), new List<System.Type>() { typeof(RulesEditor) });

        _isInitialized = true;
    }

    protected virtual void Dispose(bool isCalledManually) {
        if (_isDisposed == false) {
            if (isCalledManually) {
                // Dispose managed objects here.
            }

            // Free unmanaged resources here and set large fields to null.

            _isDisposed = true;
        }
    }
    #endregion

    #region ISharedLibraryMessengerConsumer

    public void SetSharedMessenger(ISharedLibraryMessenger sharedMessenger) {
        GlobalMessenger = sharedMessenger;
    }

    #endregion

    #region IDashboardControlProvider

    public List<System.Type> GetDashboardControls() {
        return DashboardControls;
    }

    #endregion

    #region IDashboardControlEditorProvider

    public Dictionary<System.Type, List<System.Type>> GetEditors() {
        return DashboardControlEditors;
    }

    #endregion

    #region ICacheConsumer

    public void SetCache(ICache cache) {
        Cache = cache;
    }

    #endregion

    #region IConnectionProvider

    private readonly ObservableCollection<ConnectionDefinition> _realCollection = new();
    private ConnectionDefinition _currentConnection;
    private bool _isConnected;
    private bool _isDisconnected = true;

    public IEnumerable<ConnectionDefinition> AvailableConnections { get; private set; }

    public ConnectionDefinition CurrentConnection { get { return _currentConnection; } set { SetField(ref _currentConnection, value); } }
    public bool IsConnected { get { return _isConnected; } set { SetField(ref _isConnected, value); } }
    public bool IsDisconnected { get { return _isDisconnected; } set { SetField(ref _isDisconnected, value); } }

    public void Disconnect() {
        _homieProvider.Disconnect();
        IsConnected = _homieProvider.IsConnected;
        IsDisconnected = !_homieProvider.IsConnected;
    }

    public bool TryConnect(out string errorMessage) {
        // UpdateStatusCode(3, $"VirtualDevice: trying to connect to {localIpAddress} ...");
        _homieProvider.Connect(CurrentConnection.Parameters);

        IsConnected = _homieProvider.IsConnected;
        IsDisconnected = !_homieProvider.IsConnected;

        errorMessage = "";

        return IsConnected;
    }

    #endregion

    #region IConnectionGuiProvider

    public System.Type GetGuiControl() {
        return typeof(Connection);
    }

    #endregion

    #region INotifyPropertyChanged

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(string propertyName) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = "") {
        if (EqualityComparer<T>.Default.Equals(field, value)) { return false; }

        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    #endregion
}

