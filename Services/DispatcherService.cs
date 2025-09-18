namespace LiveCommentsDemo.Services
{
    public class DispatcherService
    {
        private readonly Dictionary<string, List<string>> _roomToConnections = new();
        
        public void Register(string liveVideoId, string connectionId)
        {
            if (!_roomToConnections.ContainsKey(liveVideoId))
            {
                _roomToConnections[liveVideoId] = new List<string>();
            }
            _roomToConnections[liveVideoId].Add(connectionId);
        }

        public IEnumerable<string> GetConnections(string liveVideoId)
        {
            if (_roomToConnections.TryGetValue(liveVideoId, out var connections))
            {
                return connections;
            }
            return Enumerable.Empty<string>();
        }
    }
}
