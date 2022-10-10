namespace Tech.Tevux.Dashboards.Controls.Homie;

public partial class Indicator {
    public static readonly DependencyProperty PropertySwitcherProperty = DependencyProperty.Register(
        nameof(PropertySwitcher),
        typeof(PropertySwitcher),
        typeof(Indicator),
        new PropertyMetadata(null));

    public PropertySwitcher PropertySwitcher {
        get { return (PropertySwitcher)GetValue(PropertySwitcherProperty); }
        set { SetValue(PropertySwitcherProperty, value); }
    }
}
