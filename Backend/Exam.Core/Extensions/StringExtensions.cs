namespace Exam.Extensions;

public static class StringExtensions
{
    /// <summary>
    ///     Adds a char to end of given string if it does not ends with the char.
    /// </summary>
    /// <param name="toModify"></param>
    /// <param name="c"></param>
    /// <returns></returns>
    public static string EnsureEndsWith(this string toModify, char c)
    {
        return toModify.EnsureEndsWith(c, StringComparison.Ordinal);
    }

    /// <summary>
    ///     Adds a char to end of given string if it does not ends with the char.
    /// </summary>
    /// <param name="toModify"></param>
    /// <param name="c"></param>
    /// <param name="comparisonType"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws when string is null</exception>
    private static string EnsureEndsWith(this string toModify, char c, StringComparison comparisonType)
    {
        if (toModify == null)
        {
            throw new ArgumentNullException(nameof(toModify));
        }

        if (toModify.EndsWith(c.ToString(), comparisonType))
        {
            return toModify;
        }

        return toModify + c;
    }

    /// <summary>
    ///     A cleaner way to check if a string is null or empty.
    /// </summary>
    /// <param name="toModify"></param>
    /// <returns></returns>
    public static bool IsNullOrEmpty(this string toModify)
    {
        return string.IsNullOrEmpty(toModify);
    }

    /// <summary>
    ///     Removes first occurrence of the given post strings from end of the given string.
    ///     Ordering is important. If one of the post strings is matched, others will not be
    ///     tested.
    /// </summary>
    /// postFixes:
    /// one or more postfix.
    /// <param name="toModify"></param>
    /// <param name="postStrings">One or more post strings</param>
    /// <returns>Modified string or the same string if there is no given post strings</returns>
    public static string? RemovePostString(this string? toModify, params string[] postStrings)
    {
        if (toModify == null)
        {
            return null;
        }

        if (string.IsNullOrEmpty(toModify))
        {
            return string.Empty;
        }

        if (postStrings.IsNullOrEmpty())
        {
            return toModify;
        }

        foreach (var text in postStrings)
        {
            if (toModify.EndsWith(text))
            {
                return toModify.GetSubstringFromLeft(toModify.Length - text.Length);
            }
        }

        return toModify;
    }

    /// <summary>
    ///     Gets the substring of a string from beginning of the string to given length.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     Thrown if str is null
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     Thrown if given length is bigger than toModify length
    /// </exception>
    /// <param name="toModify">String to get substring</param>
    /// <param name="length">Length to get from str</param>
    public static string GetSubstringFromLeft(this string? toModify, int length)
    {
        if (toModify == null)
        {
            throw new ArgumentNullException(nameof(toModify));
        }

        if (toModify.Length < length)
        {
            throw new ArgumentException("len argument can not be bigger than given string's length!");
        }

        return toModify[..length];
    }
}