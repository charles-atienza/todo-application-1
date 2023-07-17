namespace Exam;

/// <summary>
///     Guard helper
/// </summary>
public class GuardHelper
{
    /// <summary>
    ///     Checks if the given value is null and throws an ArgumentNullException if it is.
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="message"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void ThrowIfNull(object? obj, string message)
    {
        if (obj == null)
        {
            throw new ArgumentException(message);
        }
    }

    /// <summary>
    ///     Throws an exception if the string is null or empty.
    /// </summary>
    /// <param name="condition"></param>
    /// <param name="message"></param>
    /// <exception cref="ArgumentException"></exception>
    public static void ThrowIf(bool condition, string message)
    {
        if (condition)
        {
            throw new ArgumentException(message);
        }
    }

    /// <summary>
    ///     Throws an exception with the given message.
    /// </summary>
    /// <param name="message"></param>
    /// <exception cref="Exception"></exception>
    public static void Throw(string message)
    {
        throw new ArgumentException(message);
    }
}