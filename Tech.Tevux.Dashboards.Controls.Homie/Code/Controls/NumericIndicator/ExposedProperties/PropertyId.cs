namespace Tech.Tevux.Dashboards.Controls.Homie;

public partial class NumericIndicator {
    public static readonly DependencyProperty PropertyIdProperty = DependencyProperty.Register(
        nameof(PropertyId),
        typeof(string),
        typeof(NumericIndicator),
        new PropertyMetadata("no-property", (obj, e) => {
            ((NumericIndicator)obj).UpdateHomiePropertyMetadata();
        }));

    [ExposedSingleLineText]
    [Category("Homie")]
    public string PropertyId {
        get { return (string)GetValue(PropertyIdProperty); }
        set { SetValue(PropertyIdProperty, value); }
    }
}

