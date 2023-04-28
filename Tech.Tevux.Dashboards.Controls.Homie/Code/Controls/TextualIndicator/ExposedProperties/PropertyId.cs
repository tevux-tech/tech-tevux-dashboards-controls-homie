namespace Tech.Tevux.Dashboards.Controls.Homie;

public partial class TextualIndicator {
    public static readonly DependencyProperty PropertyIdProperty = DependencyProperty.Register(
        nameof(PropertyId),
        typeof(string),
        typeof(TextualIndicator),
        new PropertyMetadata("no-property", (obj, e) => {
            ((TextualIndicator)obj).UpdateHomiePropertyMetadata();
        }));

    [ExposedOption(OptionType.SingleLineText)]
    [Category("Homie")]
    public string PropertyId {
        get { return (string)GetValue(PropertyIdProperty); }
        set { SetValue(PropertyIdProperty, value); }
    }
}

