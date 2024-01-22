using System.Runtime.CompilerServices;

namespace Tech.Tevux.Dashboards.Controls.Homie;

public partial class Connection {
    private void OnPropertyChanged(string propertyName) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = "") {
        if (EqualityComparer<T>.Default.Equals(field, value)) { return false; }

        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    #region IConnection Members

    public event PropertyChangedEventHandler? PropertyChanged = delegate { };

    #endregion
}
