namespace Tech.Tevux.Dashboards.Controls.Homie;

public partial class Connection {
    public static readonly DependencyProperty IsDisconnectedProperty = DependencyProperty.Register(
        nameof(IsDisconnected),
        typeof(bool),
        typeof(Connection),
        new PropertyMetadata(true));

    public bool IsDisconnected {
        get { return (bool)GetValue(IsDisconnectedProperty); }
        set { SetValue(IsDisconnectedProperty, value); }
    }
}
