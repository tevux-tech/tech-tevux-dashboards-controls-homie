namespace Tech.Tevux.Dashboards.Controls.Homie;

public partial class Connection {
    public static readonly DependencyProperty IsConnectedProperty = DependencyProperty.Register(
        nameof(IsConnected),
        typeof(bool),
        typeof(Connection),
        new PropertyMetadata(false));

    public bool IsConnected {
        get { return (bool)GetValue(IsConnectedProperty); }
        set { SetValue(IsConnectedProperty, value); }
    }
}
