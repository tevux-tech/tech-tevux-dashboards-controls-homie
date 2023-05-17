namespace Tech.Tevux.Dashboards.Controls.Homie;

[HideExposedOption(nameof(Caption))]
[Category("Homie")]
public partial class NumericIndicator : NumericOutputControlBase, IHomieTopicPath {
    private bool _isDisposed;

    static NumericIndicator() {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(NumericIndicator), new FrameworkPropertyMetadata(typeof(NumericIndicator)));
    }

    public NumericIndicator() {
        if (DesignerProperties.GetIsInDesignMode(new DependencyObject())) { return; }

        PropertySwitcher = new PropertySwitcher(HandleHomieValueChanged);
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
        PropertySwitcher.UpdateHomiePropertyMetadata(DeviceId, NodeId, PropertyId, out var errorMessage);
        ErrorMessage = errorMessage;
    }

    private void HandleHomieValueChanged() {
        Dispatcher.Invoke(() => {
            switch (PropertySwitcher.HomieProperty) {
                case ClientNumberProperty numberProperty:
                    NumericValue = (decimal)numberProperty.Value;
                    break;

                case ClientTextProperty textProperty:
                case ClientChoiceProperty choiceProperty:
                    ErrorMessage = "Device return non-numeric values";
                    break;
            }
        });
    }
}
