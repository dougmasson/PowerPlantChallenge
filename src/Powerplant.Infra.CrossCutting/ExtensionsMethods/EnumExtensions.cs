using System;
using System.ComponentModel;
using System.Reflection;

namespace Powerplant.Infra.CrossCutting
{
    public static class EnumExtensions
    {
        public static T ToEnum<T>(this string value)
        {
            if (Enum.IsDefined(typeof(T), value))
            {
                return (T)Enum.Parse(typeof(T), value, true);
            }
            else
            {
                string[] enumNames = Enum.GetNames(typeof(T));
                foreach (string enumName in enumNames)
                {
                    object e = Enum.Parse(typeof(T), enumName);
                    if (value == ((Enum)e).ToDescriptionString())
                    {
                        return (T)e;
                    }
                }
            }

            throw new ArgumentException("The value '" + value + "' does not match a valid enum name or description.");
        }

        public static string ToDescriptionString<TEnum>(this TEnum @enum)
        {
            FieldInfo info = @enum.GetType().GetField(@enum.ToString());
            var attributes = (DescriptionAttribute[])info.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes?[0].Description ?? @enum.ToString();
        }
    }
}
