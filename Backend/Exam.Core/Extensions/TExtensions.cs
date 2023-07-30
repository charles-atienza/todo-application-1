namespace Exam.Extensions;

public static class CollectionTExtension
{
    /// <summary>
    ///     A cleaner way to check if ICollection<T> is null or empty.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static bool IsNullOrEmpty<T>(this ICollection<T> source)
    {
        if (source != null)
        {
            return source.Count <= 0;
        }

        return true;
    }

    /// <summary>
    ///     Adds an item to the collection if it's not already in the collection.
    /// </summary>
    /// <typeparam name="T">Type of the items in the collection</typeparam>
    /// <param name="source"></param>
    /// <param name="item">Item to check and add</param>
    /// <returns>Returns True if added, returns False if not.</returns>
    /// <exception cref="ArgumentNullException">Throws when source is null.</exception>
    public static bool AddIfNotContains<T>(this ICollection<T> source, T item)
    {
        if (source == null)
        {
            throw new ArgumentNullException("source");
        }

        if (source.Contains(item))
        {
            return false;
        }

        source.Add(item);
        return true;
    }
}

public static class IDictionaryTExtension
{
    /// <summary>
    ///     Get the value from the dictionary with the given key. Returns default value if not found.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="dictionary">Dictionary to check and get</param>
    /// <param name="key">Key to find the value</param>
    /// <param name="factory">A factory method used to create the value if not found in the dictionary</param>
    /// <returns>Returns TValue if key found else return default.</returns>
    public static TValue GetOrAdd<TKey, TValue>(
        this IDictionary<TKey, TValue> dictionary,
        TKey key,
        Func<TValue> factory
    )
    {
        return dictionary.GetOrAdd(key, k => factory());
    }

    /// <summary>
    ///     Get the value from the dictionary with the given key. Returns default value if not found.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="dictionary">Dictionary to check and get from</param>
    /// <param name="key">The key used for dictionary to get value from</param>
    /// <param name="factory">The factory method used to create the value if not found in the dictionary</param>
    /// <returns>Returns TValue if key found else return default.</returns>
    public static TValue GetOrAdd<TKey, TValue>(
        this IDictionary<TKey, TValue> dictionary,
        TKey key,
        Func<TKey, TValue> factory
    )
    {
        if (dictionary.TryGetValue(key, out var value))
        {
            return value;
        }

        return dictionary[key] = factory(key);
    }
}