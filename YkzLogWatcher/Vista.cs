using System.Collections.Concurrent;

namespace YkzWorkHelper
{
    static class Vista
    {
        public static ConcurrentDictionary<string, string> accesosDirectos = new ConcurrentDictionary<string, string>();
        public static ConcurrentDictionary<string, bool> filtros = new ConcurrentDictionary<string, bool>();

        static Vista()
        {

        }
    }
}
