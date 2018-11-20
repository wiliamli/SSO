using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Framework.Extensions
{
    public static class EnumExtension
    {
        /// <summary>  
        /// 获取枚举项描述信息
        /// </summary>  
        /// <param name="en">枚举项</param>  
        /// <returns></returns>
        public static string EnumDesc(this Enum en)
        {
            string display = string.Empty;

            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                    display = ((DescriptionAttribute)attrs[0]).Description;
                else
                    throw new Exception("没有设置Description属性");
            }

            return display;
        }

        /// <summary>
        /// 枚举列表化
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public static Dictionary<string, string> ToKeyValue(Enum en)
        {
            Dictionary<string, string> kv = new Dictionary<string, string>();
            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                string description = string.Empty;
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                {
                    description = ((DescriptionAttribute)attrs[0]).Description;
                    memInfo.Each(m =>
                    {
                        kv.Add(m.Name, description);
                    });
                }
            }
            return kv;
        }
    }
}
