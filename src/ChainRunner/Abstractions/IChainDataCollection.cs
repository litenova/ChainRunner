namespace ChainRunner;

public interface IChainDataCollection 
{
    /// <summary>
    /// Retrieves the requested item from the collection.
    /// </summary>
    /// <param name="key"></param>
    /// <typeparam name="TItem">The item key.</typeparam>
    /// <returns>The requested item, or default if it is not present.</returns>
    TItem? Get<TItem>(string key);

    /// <summary>
    /// Sets the given item in the collection.
    /// </summary>
    /// <typeparam name="TItem">The item value type</typeparam>
    /// <param name="key">The item key.</param>
    /// <param name="instance">The item value.</param>
    void Set<TItem>(string key, TItem instance);

    /// <summary>
    /// check whether a particular key exists.
    /// </summary>
    /// <param name="key">The key</param>
    /// <returns>returns true if the key exists</returns>
    bool ContainsKey(string key);
}