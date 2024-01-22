namespace Tech.Tevux.Dashboards.Controls.Homie;

public partial class TimeChart {
    public static readonly DependencyProperty DataRetentionIntervalProperty = DependencyProperty.Register(
        nameof(DataRetentionInterval),
        typeof(decimal),
        typeof(TimeChart),
        new PropertyMetadata(48m));

    [ExposedNumber]
    [DisplayName("Retention (hours)")]
    [Category("Configuration")]
    public decimal DataRetentionInterval {
        get { return (decimal)GetValue(DataRetentionIntervalProperty); }
        set { SetValue(DataRetentionIntervalProperty, value); }
    }
}
