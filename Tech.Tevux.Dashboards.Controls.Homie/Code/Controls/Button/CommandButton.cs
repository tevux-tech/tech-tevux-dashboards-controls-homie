using DevBot9.Mvvm;

namespace Tech.Tevux.Dashboards.Controls.Homie;

[Category("Homie")]
public partial class CommandButton : ControlBase, IHomieTopicPath {
    private bool _isDisposed;

    static CommandButton() {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(CommandButton), new FrameworkPropertyMetadata(typeof(CommandButton)));
    }

    public CommandButton() {
        if (DesignerProperties.GetIsInDesignMode(new DependencyObject())) { return; }

        PropertySwitcher = new PropertySwitcher(() => { });

        ExecuteCommand = new DelegateCommand(() => {
            switch (PropertySwitcher.HomieProperty) {
                case ClientTextProperty textProperty:
                    textProperty.Value = Value;
                    break;

                case ClientChoiceProperty choiceProperty:
                    choiceProperty.Value = Value;
                    break;
            }
        }, () => true);
    }

    protected override void Dispose(bool isCalledManually) {
        if (_isDisposed == false) {
            if (isCalledManually) {
                PropertySwitcher?.Dispose();
            }

            // Free unmanaged resources here and set large fields to null.
            _isDisposed = true;
        }

        base.Dispose(isCalledManually);
    }

    protected virtual void UpdateHomiePropertyMetadata() {
        PropertySwitcher.UpdateHomiePropertyMetadata(DeviceId, NodeId, PropertyId, out var _);
    }
}
