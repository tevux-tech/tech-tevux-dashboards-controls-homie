namespace Tech.Tevux.Dashboards.Controls.Homie;

public class LibrarySupervisor : ISharedLibraryMessengerConsumer {
    internal ISharedLibraryMessenger GlobalMessenger { get; private set; } = new EmptyLibraryMessenger();
    private bool _isInitialized;
    public static LibrarySupervisor Instance { get; } = new LibrarySupervisor();

    public void Initialize() {
        if (_isInitialized) { return; }


        _isInitialized = true;
    }

    public void GetFromCache(object owner, string propertyName, out object value) {
        var getMessage = new GetCachedValueMessage();
        getMessage.Key = $"cache.{owner.GetType().FullName}.{propertyName}";
        GlobalMessenger.Send(getMessage);
        for (var i = 0; i < 25; i++) {
            // Only waiting for a limited amount of time. This is by design. Script messenger is for instantanuous stuff only.
            if (getMessage.IsFinished) { break; }

            Thread.Sleep(1);
        }
        value = getMessage.Value!;
    }

    public void SetToCache(object owner, string propertyName, object value) {
        var getMessage = new SetCachedValueMessage();
        getMessage.Key = $"cache.{owner.GetType().FullName}.{propertyName}";
        getMessage.Value = value;
        GlobalMessenger.Send(getMessage);
    }

    public void SetSharedMessenger(ISharedLibraryMessenger sharedMessenger) {
        GlobalMessenger = sharedMessenger;
    }
}

