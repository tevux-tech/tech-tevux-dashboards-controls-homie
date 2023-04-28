namespace Tech.Tevux.Dashboards.Controls.Homie;

public partial class NumericIndicator {
    public static readonly DependencyProperty PropertySwitcherProperty = DependencyProperty.Register(
        nameof(PropertySwitcher),
        typeof(PropertySwitcher),
        typeof(NumericIndicator),
        new PropertyMetadata(null));

    public PropertySwitcher PropertySwitcher {
        get { return (PropertySwitcher)GetValue(PropertySwitcherProperty); }
        set { SetValue(PropertySwitcherProperty, value); }
    }
}
