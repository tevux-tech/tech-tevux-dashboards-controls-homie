namespace Tech.Tevux.Dashboards.Controls.Homie;

public partial class Button {
    public static readonly DependencyProperty NodeIdProperty = DependencyProperty.Register(
        nameof(NodeId),
        typeof(string),
        typeof(Button),
        new PropertyMetadata("no-node", (obj, e) => {
            ((Button)obj).UpdateHomiePropertyMetadata();
        }));

    [ExposedOption(OptionType.SingleLineText)]
    [Category("Homie")]
    public string NodeId {
        get { return (string)GetValue(NodeIdProperty); }
        set { SetValue(NodeIdProperty, value); }
    }
}
