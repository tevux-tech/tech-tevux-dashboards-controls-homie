namespace Tech.Tevux.Dashboards.Controls.Homie;

public partial class NumericIndicator {
    public static readonly DependencyProperty NodeIdProperty = DependencyProperty.Register(
        nameof(NodeId),
        typeof(string),
        typeof(NumericIndicator),
        new PropertyMetadata("no-node", (obj, e) => {
            ((NumericIndicator)obj).UpdateHomiePropertyMetadata();
        }));

    [ExposedOption(OptionType.SingleLineText)]
    [Category("Homie")]
    public string NodeId {
        get { return (string)GetValue(NodeIdProperty); }
        set { SetValue(NodeIdProperty, value); }
    }
}
