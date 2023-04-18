﻿namespace Tech.Tevux.Dashboards.Controls.Homie;

[HideExposedOption(nameof(Caption))]
[Category("Homie")]
public partial class Indicator : TextualOutputControlBase {
    private bool _isDisposed;

    static Indicator() {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Indicator), new FrameworkPropertyMetadata(typeof(Indicator)));
    }

    public Indicator() {
        if (DesignerProperties.GetIsInDesignMode(new DependencyObject())) { return; }

        PropertySwitcher = new PropertySwitcher(HandleHomieValueChanged);
    }

    protected override void Dispose(bool isCalledManually) {
        if (_isDisposed == false) {
            if (isCalledManually) {
                if (PropertySwitcher != null) {
                    PropertySwitcher.Dispose();
                }
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
                    Caption = textProperty.Value;
                    break;

                case ClientNumberProperty numberProperty:
                    ApplyAppearanceRules((decimal)numberProperty.Value);
                    break;

                case ClientChoiceProperty choiceProperty:
                    ApplyAppearanceRules(choiceProperty.Value);
                    break;
            }
        });
    }
}
