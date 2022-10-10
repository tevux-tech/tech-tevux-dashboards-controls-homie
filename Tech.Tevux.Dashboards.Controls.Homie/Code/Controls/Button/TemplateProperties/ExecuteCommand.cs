namespace Tech.Tevux.Dashboards.Controls.Homie;

public partial class Button {
    public static readonly DependencyProperty ExecuteCommandProperty = DependencyProperty.Register(
        nameof(ExecuteCommand),
        typeof(ICommand),
        typeof(Button),
        new PropertyMetadata(null));

    public ICommand ExecuteCommand {
        get { return (ICommand)GetValue(ExecuteCommandProperty); }
        set { SetValue(ExecuteCommandProperty, value); }
    }
}
