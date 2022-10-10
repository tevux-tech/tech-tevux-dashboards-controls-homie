namespace Tech.Tevux.Dashboards.Controls.Homie;

public partial class Connection {
    public static readonly DependencyProperty DisconnectCommandProperty = DependencyProperty.Register(
        nameof(DisconnectCommand),
        typeof(ICommand),
        typeof(Connection),
        new PropertyMetadata(null));

    public ICommand DisconnectCommand {
        get { return (ICommand)GetValue(DisconnectCommandProperty); }
        set { SetValue(DisconnectCommandProperty, value); }
    }
}
