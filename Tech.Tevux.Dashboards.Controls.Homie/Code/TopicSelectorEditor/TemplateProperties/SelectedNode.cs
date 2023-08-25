namespace Tech.Tevux.Dashboards.Controls.Homie;

public partial class TopicSelectorEditor {
    public static readonly DependencyProperty SelectedNodeProperty = DependencyProperty.Register(
        nameof(SelectedNode),
        typeof(ClientNode),
        typeof(TopicSelectorEditor),
        new PropertyMetadata(null));

    public ClientNode SelectedNode {
        get { return (ClientNode)GetValue(SelectedNodeProperty); }
        set { SetValue(SelectedNodeProperty, value); }
    }
}
