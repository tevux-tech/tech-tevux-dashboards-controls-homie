using System.Windows.Controls;
using ScottPlot;
using ScottPlot.WPF;

namespace Tech.Tevux.Dashboards.Controls.Homie;

[Category("Homie")]
public partial class TimeChart : ControlBase {
    private readonly List<Coordinates> _points = [];
    private readonly object _pointsLock = new();
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

            lock (_pointsLock) {
                _graph.Plot.Add.Scatter(_points);
            }

            _graph.Menu.Add("Clear", _ => {
                lock (_pointsLock) {
                    _points.Clear();
                    _graph.Refresh();
                }
            });

            grid.Children.Add(_graph);
        }
    }

    private void HandleHomieValueChanged() {
        var newValue = 0.0;
        var interval = 24m;

        Dispatcher.Invoke(() => {
            if (PropertySwitcher.HomieProperty is ClientNumberProperty numberProperty) {
                newValue = numberProperty.Value;
                interval = DataRetentionInterval;
            } else {
                ErrorMessage = "Malformed data arrived.";
            }
        });

        var earliestPoint = DateTime.Now.AddHours((double)-interval).ToOADate();

        lock (_pointsLock) {
            var removeIndexTo = _points.FindIndex(x => x.X >= earliestPoint);

            if (removeIndexTo > 0) {
                _points.RemoveRange(0, removeIndexTo);
            }

            _points.Add(new Coordinates(DateTime.Now.ToOADate(), newValue));
        }

        _graph?.Plot.Axes.AutoScale();

        Dispatcher.Invoke(() => {
            _graph?.Refresh();
        });
    }

    private void UpdateHomiePropertyMetadata() {
        PropertySwitcher.UpdateHomiePropertyMetadata(DeviceId, NodeId, PropertyId, out _);

        lock (_pointsLock) {
            _points.Clear();
        }
    }
}
