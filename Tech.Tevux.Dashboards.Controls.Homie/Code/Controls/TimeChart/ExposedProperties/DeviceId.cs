namespace Tech.Tevux.Dashboards.Controls.Homie;

public partial class TimeChart {
    public static readonly DependencyProperty DeviceIdProperty = DependencyProperty.Register(
        nameof(DeviceId),
        typeof(string),
        typeof(TimeChart),
        new PropertyMetadata("no-device", (obj, e) => {
            ((TimeChart)obj).UpdateHomiePropertyMetadata();
        }));

    [ExposedSingleLineText]
    [Category("Homie")]
    public string DeviceId {
        get { return (string)GetValue(DeviceIdProperty); }
        set { SetValue(DeviceIdProperty, value); }
    }
}
