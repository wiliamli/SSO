using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Modules.WebApi.INI
{
    public class IniConfig
    {
        public static string ReadValue(string key)
        {
            string iniPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), "setup.json");
            string value = string.Empty;
            if (System.IO.File.Exists(iniPath))
            {
                value = GetPrivateProfileString(key, iniPath);

                return value;
            }
            else
            {
                throw new IniException("setup.json未找到,该文件必须存放在根目录下");
            }
        }


        private static string GetPrivateProfileString(string key, string FileName)
        {
            string value = string.Empty;
            string[] iniItems = new string[0];
            string iniLines;
            
            //读取INI文件；
            using (System.IO.StreamReader iniFile = new System.IO.StreamReader(FileName, System.Text.Encoding.Default))
            {
                iniLines = iniFile.ReadToEnd();
            }

            //以回车符分割,得到每一行
            iniItems = iniLines.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
            //遍历每一行
            for (int i = 0; i < iniItems.GetLength(0); i++)
            {
                if (!string.IsNullOrWhiteSpace(iniItems[i].Trim()))
                {
                    //找到匹配值
                    if (iniItems[i].Trim().ToUpper() == key.Trim().ToUpper())
                    {
                        value = iniItems[1].Trim();
                        break;
                    }
                }
            }
            return value;//返回默认值
        }

    }
}
