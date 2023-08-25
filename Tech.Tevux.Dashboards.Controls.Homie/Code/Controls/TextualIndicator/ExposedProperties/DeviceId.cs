namespace Tech.Tevux.Dashboards.Controls.Homie;

public partial class TextualIndicator {
    public static readonly DependencyProperty DeviceIdProperty = DependencyProperty.Register(
        nameof(DeviceId),
        typeof(string),
        typeof(TextualIndicator),
        new PropertyMetadata("no-device", (obj, e) => {
            ((TextualIndicator)obj).UpdateHomiePropertyMetadata();
        }));

    [ExposedSingleLineText]
    [Category("Homie")]
    public string DeviceId {
        get { return (string)GetValue(DeviceIdProperty); }
        set { SetValue(DeviceIdProperty, value); }
    }
}
