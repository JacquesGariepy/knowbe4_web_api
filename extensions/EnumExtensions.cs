using System.ComponentModel;
using System.Reflection;

/// <summary>
/// Extension methods for Enum
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// Get the description of the enum value
    /// </summary>
    /// <param name="value">Enum value</param>
    /// <returns>Description of the enum value</returns>
    public static string GetDescription(this Enum value)
    {
        Type type = value.GetType();
        string name = Enum.GetName(type, value);

        if (name != null)
        {
            FieldInfo fieldInfo = type.GetField(name);
            if (fieldInfo != null)
            {
                DescriptionAttribute attribute = fieldInfo.GetCustomAttribute<DescriptionAttribute>();
                if (attribute != null)
                {
                    return attribute.Description;
                }
            }
        }

        return value.ToString();
    }
}