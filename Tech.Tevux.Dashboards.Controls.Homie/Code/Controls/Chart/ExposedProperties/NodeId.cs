namespace Tech.Tevux.Dashboards.Controls.Homie;

public partial class Chart {
    public static readonly DependencyProperty NodeIdProperty = DependencyProperty.Register(
        nameof(NodeId),
        typeof(string),
        typeof(Chart),
        new PropertyMetadata("no-node", (obj, e) => {
            ((Chart)obj).UpdateHomiePropertyMetadata();
        }));

    [ExposedSingleLineText]
    [Category("Homie")]
    public string NodeId {
        get { return (string)GetValue(NodeIdProperty); }
        set { SetValue(NodeIdProperty, value); }
    }
}
