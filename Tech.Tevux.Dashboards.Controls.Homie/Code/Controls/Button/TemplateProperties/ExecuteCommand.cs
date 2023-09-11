namespace Tech.Tevux.Dashboards.Controls.Homie;

public partial class CommandButton {
    public static readonly DependencyProperty ExecuteCommandProperty = DependencyProperty.Register(
        nameof(ExecuteCommand),
        typeof(ICommand),
        typeof(CommandButton),
        new PropertyMetadata(null));

    public ICommand ExecuteCommand {
        get { return (ICommand)GetValue(ExecuteCommandProperty); }
        set { SetValue(ExecuteCommandProperty, value); }
    }
}
