using System;
using System.Collections.Generic;
using System.Text.Json;

namespace lab8.Helpers
{
    public sealed class Utils
    {
        private static readonly Lazy<Utils> _instance = new Lazy<Utils>(() => new Utils());
        public static Utils Instance => _instance.Value;
        
        // Private constructor: Singleton pattern
        private Utils() { }

        // Generic export yöntemi
        public string ExportToJson<T>(IEnumerable<T> data)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            return JsonSerializer.Serialize(data, options);
        }
    }
}