namespace Tech.Tevux.Dashboards.Controls.Homie;

public partial class Connection {
    public static readonly DependencyProperty StatusMessageProperty = DependencyProperty.Register(
        nameof(StatusMessage),
        typeof(string),
        typeof(Connection),
        new PropertyMetadata(""));

    public string StatusMessage {
        get { return (string)GetValue(StatusMessageProperty); }
        set { SetValue(StatusMessageProperty, value); }
    }
}
