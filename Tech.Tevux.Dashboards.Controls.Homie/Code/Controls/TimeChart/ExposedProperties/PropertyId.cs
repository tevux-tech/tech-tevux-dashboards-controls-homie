﻿namespace Tech.Tevux.Dashboards.Controls.Homie;

public partial class TimeChart {
    public static readonly DependencyProperty PropertyIdProperty = DependencyProperty.Register(
        nameof(PropertyId),
        typeof(string),
        typeof(TimeChart),
        new PropertyMetadata("no-property", (obj, e) => {
            ((TimeChart)obj).UpdateHomiePropertyMetadata();
        }));

    [ExposedSingleLineText]
    [Category("Homie")]
    public string PropertyId {
        get { return (string)GetValue(PropertyIdProperty); }
        set { SetValue(PropertyIdProperty, value); }
    }
}

