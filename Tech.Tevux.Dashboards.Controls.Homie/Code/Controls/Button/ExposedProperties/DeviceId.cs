namespace Tech.Tevux.Dashboards.Controls.Homie;

public partial class CommandButton {
    public static readonly DependencyProperty DeviceIdProperty = DependencyProperty.Register(
        nameof(DeviceId),
        typeof(string),
        typeof(CommandButton),
        new PropertyMetadata("no-device", (obj, e) => {
            ((CommandButton)obj).UpdateHomiePropertyMetadata();
        }));

    [ExposedSingleLineText]
    [Category("Homie")]
    public string DeviceId {
        get { return (string)GetValue(DeviceIdProperty); }
        set { SetValue(DeviceIdProperty, value); }
    }
}
