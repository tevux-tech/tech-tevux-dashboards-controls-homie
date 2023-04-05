using DevBot9.Mvvm;
using NLog;
using System.Threading.Tasks;


namespace Tech.Tevux.Dashboards.Controls.Homie;

public partial class Connection : ControlBase {

    private bool _isDisposed;
    public Connection() {
        ConnectCommand = new AsyncCommand(() => Task.Factory.StartNew(() => MyLibrary.Instance.TryConnect(out var _)), CanConnectExecute);
        DisconnectCommand = new AsyncCommand(() => Task.Factory.StartNew(() => MyLibrary.Instance.Disconnect()), CanDisconnectExecute);
    }

    public Logger Log { get; } = LogManager.GetCurrentClassLogger();

    public void Initialize() {

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

        base.Dispose(isCalledManually);
    }

    private bool CanConnectExecute() {
        if (MyLibrary.Instance.IsConnected) { return false; }

        return true;
    }

    private bool CanDisconnectExecute() {
        if (MyLibrary.Instance.IsConnected) { return true; }

        return false;
    }
}
