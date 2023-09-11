namespace Tech.Tevux.Dashboards.Controls.Homie;

public partial class CommandButton {
    public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
        nameof(Value),
        typeof(string),
        typeof(CommandButton),
        new PropertyMetadata("no-value"));

    [ExposedSingleLineText]
    [Category("Main")]
    public string Value {
        get { return (string)GetValue(ValueProperty); }
        set { SetValue(ValueProperty, value); }
    }
}
