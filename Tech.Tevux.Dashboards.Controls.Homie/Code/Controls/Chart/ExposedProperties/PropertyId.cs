namespace Tech.Tevux.Dashboards.Controls.Homie;

public partial class Chart {
    public static readonly DependencyProperty PropertyIdProperty = DependencyProperty.Register(
        nameof(PropertyId),
        typeof(string),
        typeof(Chart),
        new PropertyMetadata("no-property", (obj, e) => {
            ((Chart)obj).UpdateHomiePropertyMetadata();
        }));

    [ExposedSingleLineText]
    [Category("Homie")]
    public string PropertyId {
        get { return (string)GetValue(PropertyIdProperty); }
        set { SetValue(PropertyIdProperty, value); }
    }
}

