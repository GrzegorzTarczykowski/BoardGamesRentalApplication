using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BoardGamesRentalApplication.BLL.Extensions
{
    public static class Extensions
    {
        public static string GetDescription<T>(this T value) where T : Enum
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            object[] attributes = fi?.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return (attributes != null && attributes.Length > 0) ? ((DescriptionAttribute)attributes[0]).Description : value.ToString();
        }
    }
}
