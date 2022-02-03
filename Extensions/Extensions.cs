using System;

public static partial class Extensions
{
    /// <summary>
    ///     A T extension method that query if '@this' is not null.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if not null, false if not.</returns>
    public static bool IsNotNullOrEmpty(this String? @this)
    {
        return @this != null && @this.Length > 0;
    }

    public static bool IsPositiveNumber(this int @this)
    {
        return @this > 0;
    }

    public static object GetPropValue(object src, string propName)
 {
     return src.GetType().GetProperty(propName).GetValue(src, null);
 }
}