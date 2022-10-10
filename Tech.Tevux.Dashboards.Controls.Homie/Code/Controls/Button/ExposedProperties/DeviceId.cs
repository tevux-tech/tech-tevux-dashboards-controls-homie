namespace Tech.Tevux.Dashboards.Controls.Homie;

public partial class Button {
    public static readonly DependencyProperty DeviceIdProperty = DependencyProperty.Register(
        nameof(DeviceId),
        typeof(string),
        typeof(Button),
        new PropertyMetadata("no-device", (obj, e) => {
            ((Button)obj).UpdateHomiePropertyMetadata();
        }));

    [ExposedOption(OptionType.SingleLineText)]
    [Category("Homie")]
    public string DeviceId {
        get { return (string)GetValue(DeviceIdProperty); }
        set { SetValue(DeviceIdProperty, value); }
    }
}
