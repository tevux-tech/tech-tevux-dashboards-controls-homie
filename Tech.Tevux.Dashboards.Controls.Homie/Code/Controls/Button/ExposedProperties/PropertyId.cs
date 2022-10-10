namespace Tech.Tevux.Dashboards.Controls.Homie;

public partial class Button {
    public static readonly DependencyProperty PropertyIdProperty = DependencyProperty.Register(
        nameof(PropertyId),
        typeof(string),
        typeof(Button),
        new PropertyMetadata("no-property", (obj, e) => {
            ((Button)obj).UpdateHomiePropertyMetadata();
        }));

    [ExposedOption(OptionType.SingleLineText)]
    [Category("Homie")]
    public string PropertyId {
        get { return (string)GetValue(PropertyIdProperty); }
        set { SetValue(PropertyIdProperty, value); }
    }
}

