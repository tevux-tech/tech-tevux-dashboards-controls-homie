namespace Tech.Tevux.Dashboards.Controls.Homie;

public partial class Connection {
    private readonly ObservableCollection<ConnectionDefinition> _realDefinitionCollection = new();
    private ConnectionDefinition _currentDefinition = null!;
    private bool _isConnected;
    private bool _isDisconnected = true;

    public IEnumerable<ConnectionDefinition> AvailableDefinitions { get; private set; }
    public ConnectionDefinition CurrentDefinition { get { return _currentDefinition; } set { SetField(ref _currentDefinition, value); } }
    public bool IsConnected { get { return _isConnected; } set { SetField(ref _isConnected, value); } }
    public bool IsDisconnected { get { return _isDisconnected; } set { SetField(ref _isDisconnected, value); } }

    public void Disconnect() {
        _homieProvider.Disconnect();
        IsConnected = _homieProvider.IsConnected;
        IsDisconnected = !_homieProvider.IsConnected;
    }

    public bool TryConnect(out string errorMessage) {
        _homieProvider.Connect(CurrentDefinition.Parameters);

        IsConnected = _homieProvider.IsConnected;
        IsDisconnected = !_homieProvider.IsConnected;

        errorMessage = "";

        return IsConnected;
    }
}
