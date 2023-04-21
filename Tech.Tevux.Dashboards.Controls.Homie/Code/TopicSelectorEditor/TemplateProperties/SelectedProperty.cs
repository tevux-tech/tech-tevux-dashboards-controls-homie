namespace Tech.Tevux.Dashboards.Controls.Homie;

public partial class TopicSelectorEditor {
    public static readonly DependencyProperty SelectedPropertyProperty = DependencyProperty.Register(
        nameof(SelectedProperty),
        typeof(ClientPropertyBase),
        typeof(TopicSelectorEditor),
        new PropertyMetadata(null));

    public ClientPropertyBase SelectedProperty {
        get { return (ClientPropertyBase)GetValue(SelectedPropertyProperty); }
        set { SetValue(SelectedPropertyProperty, value); }
    }
}
