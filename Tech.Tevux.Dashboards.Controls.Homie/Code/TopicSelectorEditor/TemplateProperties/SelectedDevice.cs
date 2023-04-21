namespace Tech.Tevux.Dashboards.Controls.Homie;

public partial class TopicSelectorEditor {
    public static readonly DependencyProperty SelectedDeviceProperty = DependencyProperty.Register(
        nameof(SelectedDevice),
        typeof(ClientDevice),
        typeof(TopicSelectorEditor),
        new PropertyMetadata(null));

    public ClientDevice SelectedDevice {
        get { return (ClientDevice)GetValue(SelectedDeviceProperty); }
        set { SetValue(SelectedDeviceProperty, value); }
    }
}
