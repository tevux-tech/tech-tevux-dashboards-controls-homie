namespace Tech.Tevux.Dashboards.Controls.Homie;
public interface IHomieTopicPath {
    string DeviceId { get; set; }
    string NodeId { get; set; }
    string PropertyId { get; set; }
}
