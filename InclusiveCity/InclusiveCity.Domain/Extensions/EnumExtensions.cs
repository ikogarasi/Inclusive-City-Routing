using System.ComponentModel;

namespace InclusiveCity.Domain.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription<TEnum>(this TEnum EnumValue) where TEnum : Enum
        {
            var memberInfo = typeof(TEnum).GetMember(EnumValue.ToString()).FirstOrDefault();

            if (memberInfo != null)
            {
                var descriptionAttribute = memberInfo
                    .GetCustomAttributes(typeof(DescriptionAttribute), false)
                    .Cast<DescriptionAttribute>()
                    .FirstOrDefault();

                if (descriptionAttribute != null)
                {
                    return descriptionAttribute.Description;
                }
            }

            return EnumValue.ToString();
        }
    }
}
