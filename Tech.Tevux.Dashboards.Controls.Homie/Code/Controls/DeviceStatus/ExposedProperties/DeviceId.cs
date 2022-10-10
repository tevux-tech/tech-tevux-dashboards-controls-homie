namespace Tech.Tevux.Dashboards.Controls.Homie;

public partial class DeviceStatus {
    public static readonly DependencyProperty DeviceIdProperty = DependencyProperty.Register(
        nameof(DeviceId),
        typeof(string),
        typeof(DeviceStatus),
        new PropertyMetadata("no-device"));

    [ExposedOption(OptionType.SingleLineText)]
    [Category("Homie")]
    public string DeviceId {
        get { return (string)GetValue(DeviceIdProperty); }
        set { SetValue(DeviceIdProperty, value); }
    }
}
