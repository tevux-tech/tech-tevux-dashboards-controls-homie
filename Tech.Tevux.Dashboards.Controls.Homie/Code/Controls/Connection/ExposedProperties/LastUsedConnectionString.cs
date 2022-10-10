namespace Tech.Tevux.Dashboards.Controls.Homie;

public partial class Connection {
    public static readonly DependencyProperty LastUsedConnectionStringProperty = DependencyProperty.Register(
        nameof(LastUsedConnectionString),
        typeof(string),
        typeof(Connection),
        new PropertyMetadata(""));


    public string LastUsedConnectionString {
        get { return (string)GetValue(LastUsedConnectionStringProperty); }
        set { SetValue(LastUsedConnectionStringProperty, value); }
    }
}
