using DevBot9.Mvvm;

namespace Tech.Tevux.Dashboards.Controls.Homie;

[DashboardControl]
[Category("Homie")]
public partial class DeviceStatus : OutputControlBase {
    private bool _isDisposed = false;

    static DeviceStatus() {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(DeviceStatus), new FrameworkPropertyMetadata(typeof(DeviceStatus)));
    }

    public DeviceStatus() {
        if (DesignerProperties.GetIsInDesignMode(new DependencyObject())) { return; }

    }

    public override void OnApplyTemplate() {
        base.OnApplyTemplate();

        HomieWatcher.Instance.DeviceUpdated += HandleDeviceUpdatedMessage;

        if (HomieWatcher.Instance.TryGetClientDevice(DeviceId, out var device)) {
            ApplyAppearanceRules(device.State.ToString());
        }
    }

    protected override void Dispose(bool isCalledManually) {
        if (_isDisposed == false) {
            if (isCalledManually) {
                HomieWatcher.Instance.DeviceUpdated -= HandleDeviceUpdatedMessage;
            }

            // Free unmanaged resources here and set large fields to null.
            _isDisposed = true;
        }
    }

    private void HandleDeviceUpdatedMessage(object sender, DeviceUpdatedEventArgs deviceUpdatedEventArgs) {
        if (_isDisposed) { return; }

        Dispatcher.Invoke(() => {
            if (deviceUpdatedEventArgs.DeviceId == DeviceId) {
                if (HomieWatcher.Instance.TryGetClientDevice(DeviceId, out var device)) {
                    ApplyAppearanceRules(device.State.ToString());
                }
            };
        });
    }
}
