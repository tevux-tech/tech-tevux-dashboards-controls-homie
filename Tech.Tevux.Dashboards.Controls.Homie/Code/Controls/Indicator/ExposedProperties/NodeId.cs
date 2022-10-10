namespace Tech.Tevux.Dashboards.Controls.Homie;

public partial class Indicator {
    public static readonly DependencyProperty NodeIdProperty = DependencyProperty.Register(
        nameof(NodeId),
        typeof(string),
        typeof(Indicator),
        new PropertyMetadata("no-node", (obj, e) => {
            ((Indicator)obj).UpdateHomiePropertyMetadata();
        }));

    [ExposedOption(OptionType.SingleLineText)]
    [Category("Homie")]
    public string NodeId {
        get { return (string)GetValue(NodeIdProperty); }
        set { SetValue(NodeIdProperty, value); }
    }
}
