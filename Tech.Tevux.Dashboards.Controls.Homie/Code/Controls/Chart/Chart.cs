using System.Windows.Controls;
using ScottPlot;
using ScottPlot.WPF;

namespace Tech.Tevux.Dashboards.Controls.Homie;

[TemplatePart(Name = "PART_MainGrid", Type = typeof(Grid))]
[Category("Homie")]
public partial class Chart : ControlBase {
    private readonly List<Coordinates> _points = [];
    private WpfPlotGL _plot;

    static Chart() {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Chart), new FrameworkPropertyMetadata(typeof(Chart)));
    }

    public Chart() {
        if (DesignerProperties.GetIsInDesignMode(new DependencyObject())) { return; }

        PropertySwitcher = new PropertySwitcher(HandleHomieValueChanged);
    }


    public override void OnApplyTemplate() {
        base.OnApplyTemplate();

        if (DesignerProperties.GetIsInDesignMode(this)) { return; }

        if (Template.FindName("PART_ChartGrid", this) is Grid grid) {
            _plot = new WpfPlotGL();
            _plot.Plot.Axes.DateTimeTicks(Edge.Bottom);
            _plot.Plot.Add.Scatter(_points);

            grid.Children.Add(_plot);
        }
    }

    private void HandleHomieValueChanged() {
        Dispatcher.Invoke(() => {
            switch (PropertySwitcher.HomieProperty) {
                case ClientTextProperty textProperty:
                    break;

                case ClientNumberProperty numberProperty:
                    _points.Add(new Coordinates(DateTime.Now.ToOADate(), numberProperty.Value));

                    _plot?.Plot.Axes.AutoScaleY();
                    _plot?.Plot.Axes.AutoScaleX();

                    _plot?.Refresh();

                    break;

                case ClientChoiceProperty choiceProperty:
                    break;
            }
        });
    }

    protected virtual void UpdateHomiePropertyMetadata() {
        PropertySwitcher.UpdateHomiePropertyMetadata(DeviceId, NodeId, PropertyId, out var _);

        _points.Clear();
    }
}
