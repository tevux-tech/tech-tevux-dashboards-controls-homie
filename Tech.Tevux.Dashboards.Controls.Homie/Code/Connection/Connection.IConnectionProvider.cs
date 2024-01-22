namespace Tech.Tevux.Dashboards.Controls.Homie;

public partial class Connection {
    private readonly ObservableCollection<ConnectionDefinition> _realDefinitionCollection = [];
    private ConnectionDefinition _currentDefinition = null!;
    private bool _isConnected;
    private bool _isDisconnected = true;

    #region IConnection Members

    public IEnumerable<ConnectionDefinition> AvailableDefinitions { get; private init; }
    public ConnectionDefinition CurrentDefinition { get { return _currentDefinition; } set { SetField(ref _currentDefinition, value); } }
    public bool IsConnected { get { return _isConnected; } private set { SetField(ref _isConnected, value); } }
    public bool IsDisconnected { get { return _isDisconnected; } private set { SetField(ref _isDisconnected, value); } }

    public void Disconnect() {
        _homieProvider.Disconnect();
        IsConnected = _homieProvider.IsConnected;
        IsDisconnected = !_homieProvider.IsConnected;
    }

    public bool TryConnect(out string errorMessage) {
        _homieProvider.Connect(CurrentDefinition.Parameters);

        IsConnected = _homieProvider.IsConnected;
        IsDisconnected = !_homieProvider.IsConnected;

        if (IsDisconnected) {
            errorMessage = $"Connection to {CurrentDefinition.Parameters} failed.";
            MyLibrary.Instance.Log.LogError(errorMessage);
        } else {
            _cache.Write(this, nameof(CurrentDefinition), CurrentDefinition.Name);
            errorMessage = "";
        }

        return IsConnected;
    }

    #endregion
}
