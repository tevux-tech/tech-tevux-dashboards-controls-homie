using System.Windows.Controls;
using ScottPlot;
using ScottPlot.WPF;

namespace Tech.Tevux.Dashboards.Controls.Homie;

[Category("Homie")]
public partial class TimeChart : ControlBase {
    private readonly List<Coordinates> _points = [];
    private WpfPlotGL? _graph;

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
            _graph = new WpfPlotGL();
            _graph.Plot.Axes.DateTimeTicks(Edge.Bottom);
            _graph.Plot.Add.Scatter(_points);

            _graph.Menu.Add("Clear", _ => {
                _points.Clear();
                _graph.Refresh();
            });

            grid.Children.Add(_graph);
        }
    }

    private void HandleHomieValueChanged() {
        Dispatcher.Invoke(() => {
            if (PropertySwitcher.HomieProperty is not ClientNumberProperty numberProperty) { return; }

            _points.Add(new Coordinates(DateTime.Now.ToOADate(), numberProperty.Value));
            _graph?.Plot.Axes.AutoScale();
            _graph?.Refresh();
        });
    }

    private void UpdateHomiePropertyMetadata() {
        PropertySwitcher.UpdateHomiePropertyMetadata(DeviceId, NodeId, PropertyId, out _);

        _points.Clear();
    }
}
