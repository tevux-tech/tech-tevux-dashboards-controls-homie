using System.Windows.Controls;
using ScottPlot;
using ScottPlot.WPF;

namespace Tech.Tevux.Dashboards.Controls.Homie;

[Category("Homie")]
public partial class TimeChart : ControlBase {
    private readonly List<Coordinates> _points = [];
    private WpfPlotGL _plot;

    static TimeChart() {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(TimeChart), new FrameworkPropertyMetadata(typeof(TimeChart)));
    }

    public TimeChart() {
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
