using System.Collections.Concurrent;

namespace YkzWorkHelper
{
    static class Vista
    {
        public static ConcurrentDictionary<string, string> accesosDirectos = new ConcurrentDictionary<string, string>();
    }
}
