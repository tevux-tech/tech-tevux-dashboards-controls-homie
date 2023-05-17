namespace Tech.Tevux.Dashboards.Controls.Homie;

[HideExposedOption(nameof(Caption))]
[Category("Homie")]
public partial class TextualIndicator : TextualOutputControlBase, IHomieTopicPath {
    private bool _isDisposed;

    static TextualIndicator() {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(TextualIndicator), new FrameworkPropertyMetadata(typeof(TextualIndicator)));
    }

    public TextualIndicator() {
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
                case ClientTextProperty textProperty:
                    TextualValue = textProperty.Value;
                    break;

                case ClientNumberProperty numberProperty:
                    ErrorMessage = "Device returns non-text values";
                    break;

                case ClientChoiceProperty choiceProperty:
                    TextualValue = choiceProperty.Value;
                    break;
            }
        });
    }
}
