namespace Tech.Tevux.Dashboards.Controls.Homie;

public partial class NumericIndicator {
    public static readonly DependencyProperty DeviceIdProperty = DependencyProperty.Register(
        nameof(DeviceId),
        typeof(string),
        typeof(NumericIndicator),
        new PropertyMetadata("no-device", (obj, e) => {
            ((NumericIndicator)obj).UpdateHomiePropertyMetadata();
        }));

    [ExposedOption(OptionType.SingleLineText)]
    [Category("Homie")]
    public string DeviceId {
        get { return (string)GetValue(DeviceIdProperty); }
        set { SetValue(DeviceIdProperty, value); }
    }
}
