namespace Tech.Tevux.Dashboards.Controls.Homie;

public partial class Indicator {
    public static readonly DependencyProperty PropertyIdProperty = DependencyProperty.Register(
        nameof(PropertyId),
        typeof(string),
        typeof(Indicator),
        new PropertyMetadata("no-property", (obj, e) => {
            ((Indicator)obj).UpdateHomiePropertyMetadata();
        }));

    [ExposedOption(OptionType.SingleLineText)]
    [Category("Homie")]
    public string PropertyId {
        get { return (string)GetValue(PropertyIdProperty); }
        set { SetValue(PropertyIdProperty, value); }
    }
}

