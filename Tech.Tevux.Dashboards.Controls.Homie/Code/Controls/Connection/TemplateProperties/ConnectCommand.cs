namespace Tech.Tevux.Dashboards.Controls.Homie;

public partial class Connection {
    public static readonly DependencyProperty ConnectCommandProperty = DependencyProperty.Register(
        nameof(ConnectCommand),
        typeof(ICommand),
        typeof(Connection),
        new PropertyMetadata(null));

    public ICommand ConnectCommand {
        get { return (ICommand)GetValue(ConnectCommandProperty); }
        set { SetValue(ConnectCommandProperty, value); }
    }
}
