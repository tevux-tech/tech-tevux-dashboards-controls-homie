namespace Tech.Tevux.Dashboards.Controls.Homie;

public partial class Chart {
    public static readonly DependencyProperty DeviceIdProperty = DependencyProperty.Register(
        nameof(DeviceId),
        typeof(string),
        typeof(Chart),
        new PropertyMetadata("no-device", (obj, e) => {
            ((Chart)obj).UpdateHomiePropertyMetadata();
        }));

    [ExposedSingleLineText]
    [Category("Homie")]
    public string DeviceId {
        get { return (string)GetValue(DeviceIdProperty); }
        set { SetValue(DeviceIdProperty, value); }
    }
}
