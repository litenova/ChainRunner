using System;
using System.Collections;
using System.Collections.Generic;

namespace ChainRunner
{
    public class ChainDataCollection : IChainDataCollection
    {
        private readonly Dictionary<string, object> _data = new();

        public TItem? Get<TItem>(string key)
        {
            if (_data.ContainsKey(key))
            {
                return (TItem) _data[key];
            }

            return default;
        }

        public void Set<TItem>(string key, TItem instance)
        {
            _data[key] = instance ?? throw new ArgumentNullException(nameof(instance));
        }

        public bool ContainsKey(string key) => _data.ContainsKey(key);
    }
}