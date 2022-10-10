namespace Tech.Tevux.Dashboards.Controls.Homie;

public partial class Connection {
    public static readonly DependencyProperty StatusCodeProperty = DependencyProperty.Register(
        nameof(StatusCode),
        typeof(int),
        typeof(Connection),
        new PropertyMetadata(0));

    public int StatusCode {
        get { return (int)GetValue(StatusCodeProperty); }
        set { SetValue(StatusCodeProperty, value); }
    }
}
