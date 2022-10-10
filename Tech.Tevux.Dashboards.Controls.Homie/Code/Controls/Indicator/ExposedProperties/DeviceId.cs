namespace Tech.Tevux.Dashboards.Controls.Homie;

public partial class Indicator {
    public static readonly DependencyProperty DeviceIdProperty = DependencyProperty.Register(
        nameof(DeviceId),
        typeof(string),
        typeof(Indicator),
        new PropertyMetadata("no-device", (obj, e) => {
            ((Indicator)obj).UpdateHomiePropertyMetadata();
        }));

    [ExposedOption(OptionType.SingleLineText)]
    [Category("Homie")]
    public string DeviceId {
        get { return (string)GetValue(DeviceIdProperty); }
        set { SetValue(DeviceIdProperty, value); }
    }
}
