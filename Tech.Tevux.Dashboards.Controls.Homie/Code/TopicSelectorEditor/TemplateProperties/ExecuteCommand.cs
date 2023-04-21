namespace Tech.Tevux.Dashboards.Controls.Homie;

public partial class TopicSelectorEditor {
    public static readonly DependencyProperty ExecuteCommandProperty = DependencyProperty.Register(
        nameof(ExecuteCommand),
        typeof(ICommand),
        typeof(TopicSelectorEditor),
        new PropertyMetadata(null));

    public ICommand ExecuteCommand {
        get { return (ICommand)GetValue(ExecuteCommandProperty); }
        set { SetValue(ExecuteCommandProperty, value); }
    }
}
