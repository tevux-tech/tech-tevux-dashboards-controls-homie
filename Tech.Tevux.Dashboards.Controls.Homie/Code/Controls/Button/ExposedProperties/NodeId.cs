namespace Tech.Tevux.Dashboards.Controls.Homie;

public partial class CommandButton {
    public static readonly DependencyProperty NodeIdProperty = DependencyProperty.Register(
        nameof(NodeId),
        typeof(string),
        typeof(CommandButton),
        new PropertyMetadata("no-node", (obj, e) => {
            ((CommandButton)obj).UpdateHomiePropertyMetadata();
        }));

    [ExposedSingleLineText]
    [Category("Homie")]
    public string NodeId {
        get { return (string)GetValue(NodeIdProperty); }
        set { SetValue(NodeIdProperty, value); }
    }
}
