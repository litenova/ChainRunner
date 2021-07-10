using System;
using System.Collections;
using System.Collections.Generic;

namespace ChainRunner
{
    internal class ChainContext : IChainContext
    {
        private readonly Dictionary<string, object> _data = new();

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator() => _data.GetEnumerator();
        
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public TItem? Get<TItem>(string key)
        {
            if (_data.ContainsKey(key))
            {
                return (TItem)_data[key];
            }
            
            return default;
        }

        public void Set<TItem>(string key, TItem instance)
        {
            _data[key] = instance ?? throw new ArgumentNullException(nameof(instance));
        }
    }
}