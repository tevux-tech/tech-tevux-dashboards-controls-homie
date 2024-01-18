using System.Diagnostics;
using CommunityToolkit.Mvvm.Input;

namespace Tech.Tevux.Dashboards.Controls.Homie;

public partial class ConnectionOptions : ControlBase {
    private readonly ILogger _logger;

    public ConnectionOptions() {
        ConnectionBackend = MyLibrary.Instance.Connection;
        _logger = MyLibrary.Instance.Log;
      
        AddDefinitionCommand = new RelayCommand<string[]>(AddDefinition);
        DeleteDefinitionCommand = new RelayCommand<ConnectionDefinition>(DeleteDefinition);
    }

    public IConnection ConnectionBackend { get; }

    #region Commands

    public ICommand AddDefinitionCommand { get; private set; }
    public ICommand DeleteDefinitionCommand { get; private set; }

    private void AddDefinition(string[] parameters) {
        Debug.Assert(parameters is not null);
        Debug.Assert(parameters.Length == 2);
        Debug.Assert(string.IsNullOrEmpty(parameters[0]) is false);
        Debug.Assert(string.IsNullOrEmpty(parameters[1]) is false);

        if (ConnectionBackend.AvailableDefinitions is not ObservableCollection<ConnectionDefinition> realCollection) { return; }

        // There may be already present a definition with a default name. Appending it until a unique name is formed.
        while (realCollection.Any(r => r.Name == parameters[0])) {
            parameters[0] += "_";
        }

        realCollection.Add(new ConnectionDefinition { Name = parameters[0], Parameters = parameters[1] });
    }

    private void DeleteDefinition(ConnectionDefinition definitionToDelete) {
        if (ConnectionBackend.AvailableDefinitions is not ObservableCollection<ConnectionDefinition> realCollection) { return; }

        realCollection.Remove(definitionToDelete);
    }

    #endregion

    #region IDisposable

    private bool _isDisposed;

    protected override void Dispose(bool isCalledManually) {
        if (_isDisposed == false) {
            if (isCalledManually) {
                // Dispose managed objects here.
            }

            // Free unmanaged resources here and set large fields to null.

            _isDisposed = true;
        }

        base.Dispose(isCalledManually);
    }

    #endregion
}
