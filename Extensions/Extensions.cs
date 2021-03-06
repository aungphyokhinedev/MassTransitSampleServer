using System;
using System.Reflection;

public static partial class Extensions
{
    /// <summary>
    ///     A T extension method that query if '@this' is not null.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if not null, false if not.</returns>
    public static bool IsNotNullOrEmpty(this object? @this)
    {
        return @this != null && @this.ToString().Length > 0;
    }

    public static bool IsPositiveNumber(this int @this)
    {
        return @this > 0;
    }

   

    public static Dictionary<string, object> PropertiesFromInstance(this object @this)
    {
        if (@this == null) return null;
        Type TheType = @this.GetType();
        PropertyInfo[] Properties = TheType.GetProperties();
        Dictionary<string, object> PropertiesMap = new Dictionary<string, object>();
        foreach (PropertyInfo Prop in Properties)
        {   
            var value = @this.GetType().GetProperty(Prop.Name).GetValue(@this, null);
            if(value != null){
                PropertiesMap.Add(Prop.Name, value);
            }
            
        }
        return PropertiesMap;
    }
}