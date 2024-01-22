namespace Tech.Tevux.Dashboards.Controls.Homie;

public partial class TimeChart {
    public static readonly DependencyProperty NodeIdProperty = DependencyProperty.Register(
        nameof(NodeId),
        typeof(string),
        typeof(TimeChart),
        new PropertyMetadata("no-node", (obj, e) => {
            ((TimeChart)obj).UpdateHomiePropertyMetadata();
        }));

    [ExposedSingleLineText]
    [Category("Homie")]
    public string NodeId {
        get { return (string)GetValue(NodeIdProperty); }
        set { SetValue(NodeIdProperty, value); }
    }
}
