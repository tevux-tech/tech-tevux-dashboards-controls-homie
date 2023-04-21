using DevBot9.Mvvm;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Controls;

namespace Tech.Tevux.Dashboards.Controls.Homie;

[DisplayName("Homie topic selector")]
public partial class TopicSelectorEditor : Control, IDisposable, INotifyPropertyChanged {

    static TopicSelectorEditor() {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(TopicSelectorEditor), new FrameworkPropertyMetadata(typeof(TopicSelectorEditor)));
    }

    public TopicSelectorEditor() {
        if (DesignerProperties.GetIsInDesignMode(new DependencyObject())) { return; }
    }

    public TopicSelectorEditor(Control control) : this() {
        if (DesignerProperties.GetIsInDesignMode(new DependencyObject())) { return; }

        ExecuteCommand = new DelegateCommand(() => {
            if (control is IHomieTopicPath homieTopicPath) {
                homieTopicPath.DeviceId = SelectedDevice.DeviceId;
                var parts = SelectedProperty.PropertyId.Split('/');
                Debug.Assert(parts.Length == 2);
                homieTopicPath.NodeId = parts[0];
                homieTopicPath.PropertyId = parts[1];
            }
        }, () => {
            if (SelectedDevice is null) { return false; }
            if (SelectedNode is null) { return false; }
            if (SelectedProperty is null) { return false; }

            return true;
        });
    }

    public ObservableCollection<ClientDevice> AvailableDevices { get; } = new();

    public override void OnApplyTemplate() {
        base.OnApplyTemplate();

#warning Need to expose _devices field as property probably?
        var bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
        var devicesField = typeof(HomieWatcher).GetField("_devices", bindFlags);
        if (devicesField != null) {
            var devicesValue = devicesField.GetValue(HomieWatcher.Instance);
            if (devicesValue is ConcurrentDictionary<string, ClientDevice> devices) {
                AvailableDevices.Clear();
                foreach (var plaukas in devices) {
                    AvailableDevices.Add(plaukas.Value);
                }
            }
        }
    }

    #region IDisposable

    private bool _isDisposed;

    public event PropertyChangedEventHandler? PropertyChanged;

    public void Dispose() {
        // A good article explaining how to implement Dispose. https://docs.microsoft.com/en-us/dotnet/standard/garbage-collection/implementing-dispose
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool isCalledManually) {
        if (_isDisposed == false) {
            if (isCalledManually) {
                // Dispose managed objects here.
                foreach (var device in AvailableDevices) {
                    device.Dispose();
                }
                AvailableDevices.Clear();
            }

            // Free unmanaged resources here and set large fields to null.

            _isDisposed = true;
        }
    }

    #endregion
}
