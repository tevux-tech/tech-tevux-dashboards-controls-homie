namespace Tech.Tevux.Dashboards.Controls.Homie;

public class MyLibrary : ILibrary,
                         IDashboardControlProvider,
                         IDashboardControlEditorProvider,
                         IConnectionProvider {

    private bool _isInitialized;
    Connection _connectionFrontBackEnd = null!;
    private MyLibrary() {
        DashboardControls.Add(typeof(CommandButton));
        DashboardControls.Add(typeof(DeviceStatus));
        DashboardControls.Add(typeof(TextualIndicator));
        DashboardControls.Add(typeof(NumericIndicator));
        DashboardControls.Add(typeof(Chart));

        DashboardControlEditors.Add(typeof(TextualIndicator), new List<Type> { typeof(TopicSelectorEditor) });
        DashboardControlEditors.Add(typeof(NumericIndicator), new List<Type> { typeof(TopicSelectorEditor) });
        DashboardControlEditors.Add(typeof(CommandButton), new List<Type> { typeof(TopicSelectorEditor) });

        ConnectionOptionsControl = typeof(ConnectionOptions);
    }

    public static MyLibrary Instance { get; } = new MyLibrary();

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
    public Dictionary<System.Type, List<System.Type>> DashboardControlEditors { get; private set; } = new();
    public List<System.Type> DashboardControls { get; private set; } = new();
    public object ConnectionGuiControl => _connectionFrontBackEnd;

    public Type ConnectionOptionsControl { get; private set; }

    #endregion

    #region ILibrary

    public void Initialize() {
        if (_isInitialized) { return; }

        _connectionFrontBackEnd = new Connection();

        _isInitialized = true;
    }

    #endregion

    #region IDisposable

    private bool _isDisposed;

    public void Dispose() {
        // A good article explaining how to implement Dispose. https://docs.microsoft.com/en-us/dotnet/standard/garbage-collection/implementing-dispose
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool isCalledManually) {
        if (_isDisposed == false) {
            if (isCalledManually) {
                // Dispose managed objects here.
                _connectionFrontBackEnd.Dispose();
            }

            // Free unmanaged resources here and set large fields to null.

            _isDisposed = true;
        }
    }
    #endregion
}

