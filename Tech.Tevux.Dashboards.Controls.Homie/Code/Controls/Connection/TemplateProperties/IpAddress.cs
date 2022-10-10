namespace Tech.Tevux.Dashboards.Controls.Homie;

public partial class Connection {
    public static readonly DependencyProperty IpAddressProperty = DependencyProperty.Register(
        nameof(IpAddress),
        typeof(string),
        typeof(Connection),
        new PropertyMetadata("127.0.0.1"));

    public string IpAddress {
        get { return (string)GetValue(IpAddressProperty); }
        set { SetValue(IpAddressProperty, value); }
    }
}
