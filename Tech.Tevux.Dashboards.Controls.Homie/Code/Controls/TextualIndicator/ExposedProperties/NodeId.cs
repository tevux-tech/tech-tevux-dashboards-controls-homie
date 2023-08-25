namespace Tech.Tevux.Dashboards.Controls.Homie;

public partial class TextualIndicator {
    public static readonly DependencyProperty NodeIdProperty = DependencyProperty.Register(
        nameof(NodeId),
        typeof(string),
        typeof(TextualIndicator),
        new PropertyMetadata("no-node", (obj, e) => {
            ((TextualIndicator)obj).UpdateHomiePropertyMetadata();
        }));

    [ExposedSingleLineText]
    [Category("Homie")]
    public string NodeId {
        get { return (string)GetValue(NodeIdProperty); }
        set { SetValue(NodeIdProperty, value); }
    }
}
