namespace Tech.Tevux.Dashboards.Controls.Homie;

public partial class Connection {
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
                Disconnect();
                _realDefinitionCollection.CollectionChanged -= HandleAvailableDefinitionsChangedEvent;
                _homieProvider = null!;
            }

            // Free unmanaged resources here and set large fields to null.

            _isDisposed = true;
        }
    }
}
