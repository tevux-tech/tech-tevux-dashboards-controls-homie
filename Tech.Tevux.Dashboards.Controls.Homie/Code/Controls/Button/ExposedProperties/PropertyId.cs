namespace Tech.Tevux.Dashboards.Controls.Homie;

public partial class CommandButton {
    public static readonly DependencyProperty PropertyIdProperty = DependencyProperty.Register(
        nameof(PropertyId),
        typeof(string),
        typeof(CommandButton),
        new PropertyMetadata("no-property", (obj, e) => {
            ((CommandButton)obj).UpdateHomiePropertyMetadata();
        }));

    [ExposedSingleLineText]
    [Category("Homie")]
    public string PropertyId {
        get { return (string)GetValue(PropertyIdProperty); }
        set { SetValue(PropertyIdProperty, value); }
    }
}

