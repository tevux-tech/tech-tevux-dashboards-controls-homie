namespace Tech.Tevux.Dashboards.Controls.Homie;

public partial class DeviceStatus {
    public static readonly DependencyProperty DeviceStateProperty = DependencyProperty.Register(
        nameof(DeviceState),
        typeof(string),
        typeof(DeviceStatus),
        new PropertyMetadata(null));

    public string DeviceState {
        get { return (string)GetValue(DeviceStateProperty); }
        set { SetValue(DeviceStateProperty, value); }
    }
}
