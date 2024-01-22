namespace Tech.Tevux.Dashboards.Controls.Homie;

public sealed class MyLibrary : ILibrary,
    IDashboardControlProvider,
    IDashboardControlEditorProvider,
    IConnectionProvider {
    Connection _connectionFrontBackEnd = null!;

    private bool _isInitialized;

    private MyLibrary() {
        DashboardControls.Add(typeof(CommandButton));
        DashboardControls.Add(typeof(DeviceStatus));
        DashboardControls.Add(typeof(TextualIndicator));
        DashboardControls.Add(typeof(NumericIndicator));
        DashboardControls.Add(typeof(TimeChart));

        DashboardControlEditors.Add(typeof(TextualIndicator), [typeof(TopicSelectorEditor)]);
        DashboardControlEditors.Add(typeof(NumericIndicator), [typeof(TopicSelectorEditor)]);
        DashboardControlEditors.Add(typeof(CommandButton), [typeof(TopicSelectorEditor)]);

        ConnectionOptionsControl = typeof(ConnectionOptions);
    }

    public static MyLibrary Instance { get; } = new();

    #region ILibrary Members

    public void Initialize() {
        if (_isInitialized) { return; }

        _connectionFrontBackEnd = new Connection();

        _isInitialized = true;
    }

    #endregion

    #region Dependency injection

    [InjectedByHost]
    public ICacheProvider Cache { get; set; } = new EmptyCacheProvider();

    [InjectedByHost]
    public ISharedLibraryMessagingProvider GlobalMessenger { get; set; } = new EmptySharedLibraryMessagingProvider();

    [InjectedByHost]
    public ILogger Log { get; set; } = NullLogger.Instance;

    #endregion

    #region Dependency providers

    public IConnection Connection => _connectionFrontBackEnd;
    public Dictionary<Type, List<Type>> DashboardControlEditors { get; } = [];
    public List<Type> DashboardControls { get; } = [];
    public Type ConnectionOptionsControl { get; }

    #endregion

    #region IDisposable

    private bool _isDisposed;

    public void Dispose() {
        // A good article explaining how to implement Dispose. https://docs.microsoft.com/en-us/dotnet/standard/garbage-collection/implementing-dispose
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    void Dispose(bool isCalledManually) {
        if (_isDisposed) { return; }
        if (isCalledManually) {
            // Dispose managed objects here.
            _connectionFrontBackEnd.Dispose();
        }

        // Free unmanaged resources here and set large fields to null.

        _isDisposed = true;
    }

    #endregion
}
