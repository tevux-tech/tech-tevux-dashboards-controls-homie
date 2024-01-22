using System.Collections.Specialized;

namespace Tech.Tevux.Dashboards.Controls.Homie;

public sealed partial class Connection : IDisposable, IConnection {
    private readonly ICacheProvider _cache;
    private HomieWatcher _homieProvider;

    public Connection() {
        AvailableDefinitions = _realDefinitionCollection;

        _homieProvider = HomieWatcher.Instance;
        _cache = MyLibrary.Instance.Cache;

        _realDefinitionCollection.CollectionChanged += HandleAvailableDefinitionsChangedEvent;

        // Trying to read definitions from cache. Lots of failure points here.
        if (_cache.TryRead(this, nameof(AvailableDefinitions), out Dictionary<string, string> definitionDictionary)) {
            // User has something defined in the file.
            foreach (var definition in definitionDictionary) {
                _realDefinitionCollection.Add(new ConnectionDefinition { Name = definition.Key, Parameters = definition.Value });
            }
        }

        if (_realDefinitionCollection.Count == 0) {
            // There nothing in the file, so adding default options.
            _realDefinitionCollection.Add(new ConnectionDefinition { Name = "default", Parameters = "127.0.0.1" });
            _cache.Write(this, nameof(AvailableDefinitions), AvailableDefinitions.ToDictionary(d => d.Name, d => d.Parameters));
        }


        // Trying to load last used connection.
        if (_cache.TryRead(this, nameof(CurrentDefinition), out string nameString)) {
            if (_realDefinitionCollection.Any(c => c.Name == nameString)) {
                // Found one, selecting it.
                CurrentDefinition = _realDefinitionCollection.Single(b => b.Name == nameString);
            }
        }

        if (CurrentDefinition is null) {
            // Selecting first available connection, if any.
            if (_realDefinitionCollection.Count > 0) {
                CurrentDefinition = _realDefinitionCollection[0];
            }
        }
    }

    private void HandleAvailableDefinitionsChangedEvent(object? sender, NotifyCollectionChangedEventArgs e) {
        _cache.Write(this, nameof(AvailableDefinitions), AvailableDefinitions.ToDictionary(d => d.Name, d => d.Parameters));
    }
}
